using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;
using Microsoft.Extensions.Configuration;
using StremoCloud.Domain.Entities;
using StremoCloud.Infrastructure.Data;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared.Helpers;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Command.Create;

public class SignInCommand : IRequest<GenericResponse<LoginResponse>>
{
    [Required]
    [EmailAddress] //Soon to use Fluent Validation Here
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}

public class SignInCommandHandler(IConfiguration configuration, IStremoUnitOfWork unitOfWork, ITokenHelper tokenHelper)
    : IRequestHandler<SignInCommand, GenericResponse<LoginResponse>>
{
    
    public async Task<GenericResponse<LoginResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var tokenObject = new LoginResponse();
        var response = new GenericResponse<LoginResponse>
        {
            Data = tokenObject
        };
        
        request.Email = request.Email.Trim();
        var user = await unitOfWork.Repository<User>().GetByEmailAsync(request.Email);
        if (user == null)
        {
            response.Message = "Email Not Found. Please try again.";
            return response;
        }
        
        var isPasswordCorrect = HashHelper.Verify(request.Password, user.Password);
        if (!isPasswordCorrect)
        {
            response.Message = "Wrong Password. Please try again.";
            return response;
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.FirstName),
            new(ClaimTypes.Sid, user.Id)
        };
        
        tokenObject.AccessToken = tokenHelper.GenerateAccessToken(claims);
        var oldRefreshToken = await unitOfWork.Repository<RefreshToken>().FirstOrDefaultAsync(x => x.UserId == user.Id);
        if (oldRefreshToken != null)
        {
            await unitOfWork.Repository<RefreshToken>().DeleteAsync(oldRefreshToken.Id);
        }
        tokenObject.RefreshToken = tokenHelper.GenerateRefreshToken();      
        var expiresInDays = Convert.ToInt32(configuration["RefreshToken:ExpiresIn"]);
        var now = DateTime.UtcNow;
        var newRefreshToken = new RefreshToken
        {
            UserId = user.Id,
            DateCreated = now,
            DateExpires = now.AddDays(expiresInDays),
            RefreshTokenValue = tokenObject.RefreshToken
        };

        await unitOfWork.Repository<RefreshToken>().CreateAsync(newRefreshToken);
        response.IsSuccess = true;
        response.Message = "Login Successful";
        response.Data = tokenObject;
        return response;
    }
}
using Amazon.Runtime.Internal;
using MediatR;
using Microsoft.Extensions.Configuration;
using StremoCloud.Domain.Entities;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared.Helpers;
using StremoCloud.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StremoCloud.Application.Features.Command.Create;

public record RegenerateTokenCommand(string OldAccessToken, string OldRefreshToken, string UserId) : IRequest<GenericResponse<LoginResponse>>;

public class RegenerateTokenCommandHandler(IConfiguration configuration, IStremoUnitOfWork unitOfWork, ITokenHelper tokenHelper) : IRequestHandler<RegenerateTokenCommand, GenericResponse<LoginResponse>>
{
    public async Task<GenericResponse<LoginResponse>> Handle(RegenerateTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = tokenHelper.GetPrincipalFromExpiredToken(request.OldAccessToken);
        if (principal == null)
        {
            return new GenericResponse<LoginResponse>
            {
                Message = "Something went wrong in getting the claims from the principal",
                IsSuccess = false
            };
        }

        //Delete old record
        var oldRefreshToken = await unitOfWork.Repository<RefreshToken>().FirstOrDefaultAsync(x => x.UserId == request.UserId);
        if (oldRefreshToken != null)
        {
            await unitOfWork.Repository<RefreshToken>().DeleteAsync(oldRefreshToken.Id);
        }

        //Regenerate new Tokens
        string accessToken = tokenHelper.GenerateAccessToken(principal.Claims);
        string refreshToken = tokenHelper.GenerateRefreshToken();

        //Persists Refresh Token
        var expiresInDays = Convert.ToInt32(configuration["RefreshToken:ExpiresIn"]);
        var now = DateTime.UtcNow;
        var refreshTokenRecord = new RefreshToken
        {
            UserId = request.UserId,
            DateCreated = now,
            DateExpires = now.AddDays(expiresInDays),
            RefreshTokenValue = refreshToken
        };
        await unitOfWork.Repository<RefreshToken>().CreateAsync(refreshTokenRecord);

        return new GenericResponse<LoginResponse>
        {
            Data = new LoginResponse { AccessToken = accessToken, RefreshToken = refreshToken },
            IsSuccess = true,
            Message = "Tokens returned successfully"
        };
    }
}
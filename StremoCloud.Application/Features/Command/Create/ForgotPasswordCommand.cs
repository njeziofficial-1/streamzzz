using MediatR;
using StremoCloud.Application.Services;
using StremoCloud.Domain.Entities;
using StremoCloud.Infrastructure.Data;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared.Helpers;

namespace StremoCloud.Application.Features.Command.Create;

public class ForgotPasswordCommand : IRequest<bool>
{
    public string Email { get; set; }
}

public class ForgotPasswordCommandHandler (IEmailService emailService, IStremoUnitOfWork unitOfWork, IGenericRepository<Verification> verificationRepository,ISecurityHelper securityHelper,  ITokenHelper tokenHelper)
    : IRequestHandler<ForgotPasswordCommand, bool>
{
    public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        request.Email = request.Email.Trim();
        var user = await unitOfWork.Repository<User>().GetByEmailAsync(request.Email);
        if (user != null)
        {
            var uniqueString = $"{StringHelper.GenerateRandomString(6)}|{user.Email}";
            var token = securityHelper.Encrypt(uniqueString);
            var verification = new Verification 
            { 
                Token = token, 
                UserId = user.Id
            };
            await verificationRepository.CreateAsync(verification);
            string baseUrl = $"https://stremocloud.azurewebsites.net";
            string url = $"{baseUrl}?email={request.Email}&token={token}";
            string subject = "Password Reset";
            string message = $"Your password has been reset for {user.Email}. <br/> Please click the link below to reset your password. <br> {url}";
            //Send email
            await emailService.SendToEmailAsync(user.Email, message, user.FirstName, subject);
            return true;
        }
        return false;
    }
}
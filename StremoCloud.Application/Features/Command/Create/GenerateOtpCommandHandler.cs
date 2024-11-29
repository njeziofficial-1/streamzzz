using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using MediatR;
using StremoCloud.Application.Services;
using StremoCloud.Domain.Entities;
using StremoCloud.Domain.Interface;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Command.Create;
public class GenerateOtpCommandHandler(IOtpService otpService, IEmailService emailService, IStremoUnitOfWork unitOfWork) : IRequestHandler<GenerateOtpCommand, GenericResponse<string>>
{
    public async Task<GenericResponse<string>> Handle(GenerateOtpCommand request, CancellationToken cancellationToken)
    {
        // Generate OTP using the service
        string email = request.Email.Trim().ToLower();
        var user = await unitOfWork.Repository<User>().GetByEmailAsync(email);
        if (user == null)
        {
            return new GenericResponse<string>
            {
                IsSuccess = false,
                Message = $"User with email {request.Email} does not exist"
            };
        }
        var otp = otpService.GenerateAndCacheOtp(email);
        string subject = "OTP Verification";
        string message = $"Your Stremo cloud OTP is {otp}. <br/> Please use this in verification immediately, as it will expire in 5 minutes. <br>";
        //Send email
        await emailService.SendToEmailAsync(user.Email, message, user.FirstName, subject);
        return new GenericResponse<string>
        {
            IsSuccess = true,
            Message = "Otp returned successfully, and will expire after 5 minutes",
            Data = otp
        };
    }
}


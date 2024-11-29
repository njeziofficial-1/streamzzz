using FluentValidation;
using StremoCloud.Domain.Interface;

namespace StremoCloud.Application.Features.Command.Create;

public class ValidateOtpCommandValidator : AbstractValidator<ValidateOtpCommand>
{
    private readonly IOtpService _otpService;

    public ValidateOtpCommandValidator(IOtpService otpService)
    {
        _otpService = otpService;

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Must(_otpService.IsValidEmail).WithMessage("Invalid Email format.");

        RuleFor(x => x.Otp)
            .NotEmpty().WithMessage("OTP is required.")
            .Length(6).WithMessage("OTP must be 6 digits.")
            .Must((cmd, otp) => _otpService.IsOtpValid(cmd.Email, otp))
            .WithMessage("Invalid or expired OTP.");
    }
}

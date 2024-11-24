using MediatR;
using StremoCloud.Domain.Interface;

namespace StremoCloud.Application.Features.Command.Create;

public class ValidateOtpCommandHandler : IRequestHandler<ValidateOtpCommand, bool>
{
    private readonly IOtpService _otpService;

    public ValidateOtpCommandHandler(IOtpService otpService)
    {
        _otpService = otpService;
    }

    public async Task<bool> Handle(ValidateOtpCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_otpService.ValidateOtp(request.Identifier, request.Otp));
    }
}

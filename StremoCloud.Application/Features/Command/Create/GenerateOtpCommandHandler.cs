using MediatR;
using StremoCloud.Domain.Interface;

namespace StremoCloud.Application.Features.Command.Create;
public class GenerateOtpCommandHandler : IRequestHandler<GenerateOtpCommand, string>
{
    private readonly IOtpService _otpService;

    public GenerateOtpCommandHandler(IOtpService otpService)
    {
        _otpService = otpService;
    }

    public async Task<string> Handle(GenerateOtpCommand request, CancellationToken cancellationToken)
    {
        // Generate OTP using the service
        return await _otpService.GenerateOtpAsync(request.Email);
    }
}


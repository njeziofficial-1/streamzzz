using MediatR;
using StremoCloud.Domain.Interface;

namespace StremoCloud.Application.Features.Command.Create;
public class GenerateOtpCommandHandler(IOtpService otpService) : IRequestHandler<GenerateOtpCommand, string>
{
    public async Task<string> Handle(GenerateOtpCommand request, CancellationToken cancellationToken)
    {
        // Generate OTP using the service
        return await otpService.GenerateOtpAsync(request.Email);
    }
}


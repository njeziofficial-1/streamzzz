using MediatR;

namespace StremoCloud.Application.Features.Command.Create;

public record ValidateOtpCommand(string Email, string Otp) : IRequest<bool>;


using MediatR;

namespace StremoCloud.Application.Features.Command.Create;

public record ValidateOtpCommand(string Identifier, string Otp) : IRequest<bool>;


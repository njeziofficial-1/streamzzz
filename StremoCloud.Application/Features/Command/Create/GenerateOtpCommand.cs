using MediatR;

namespace StremoCloud.Application.Features.Command.Create;

public record GenerateOtpCommand(string Email) : IRequest<string>;

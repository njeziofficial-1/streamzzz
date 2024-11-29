using MediatR;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Command.Create;

public record GenerateOtpCommand(string Email) : IRequest<GenericResponse<string>>;

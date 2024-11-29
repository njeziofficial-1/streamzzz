using MediatR;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Command.Create;

public record ValidateOtpCommand(string Email, string Otp) : IRequest<GenericResponse<bool>>;

using MediatR;
using StremoCloud.Shared.Response;


namespace StremoCloud.Application.Features.Command.Create
{
    public record CreateOrderCommand(
        DateTime DatePlaced,
        decimal Amount,
        bool IsSuccessful
        ) : IRequest<GenericResponse<string>>;
}
using MediatR;
using StremoCloud.Domain.Entities;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Queries;
public record GetOrderQuery(int pageNumber = 1, int pageSize = 10) : IRequest<GenericResponse<List<OrderResponse>>>;

public class GetOrderQueryHandler(IStremoUnitOfWork unitOfWork) : IRequestHandler<GetOrderQuery, GenericResponse<List<OrderResponse>>>
{
    public async Task<GenericResponse<List<OrderResponse>>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var orders = (await unitOfWork
            .Repository<OrderList>()
            .GetAllAsync())
            .OrderByDescending(x => x.CreatedOn)
            .ToList();

        var totalRecords = orders.Count;

        var paginatedOrders = orders.Skip((request.pageNumber - 1) * request.pageSize)
            .Take(request.pageSize)
            .Select(x => new OrderResponse
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
                Address = x.Address,
               // OrderRenderStatus = x.Status.ToString(),
            }).ToList();

        string message = paginatedOrders.Any() ? "Orders returned successfully" : "No data response for the request";
        bool isSuccess = paginatedOrders.Any();
        return new GenericResponse<List<OrderResponse>>
        {
            Data = paginatedOrders,
            Message = message,
            IsSuccess = isSuccess
        };
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using StremoCloud.Application.Features.Command.Create;
using StremoCloud.Application.Features.Queries;

namespace StremoCloud.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves all orders from the system.
    /// </summary>
    /// <returns>
    /// A list of all orders wrapped in an HTTP response.
    /// </returns>
    /// <response code="200">Returns the list of orders successfully.</response>
    /// <response code="500">An internal server error occurred while processing the request.</response>
    [HttpGet("get-all-order")]
    public async Task<IActionResult> GetAllOrder()
        => Ok(await mediator.Send(new GetOrderQuery()));

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand request)
        => Ok(await mediator.Send(request));
}

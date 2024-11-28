using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StremoCloud.Application.Features.Command.Create;
using StremoCloud.Application.Features.Queries;

namespace StremoCloud.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> SignUpAsync(SignUpCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> SignInAsync(SignInCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("get-all-users")]
    public async Task<IActionResult> GetUsers()
        => Ok(await mediator.Send(new GetUserQuery()));
}

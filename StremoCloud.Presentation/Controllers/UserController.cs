using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StremoCloud.Application.Features.Command.Create;
using StremoCloud.Application.Features.Command.Delete;
using StremoCloud.Application.Features.Queries;

namespace StremoCloud.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="command">The command containing user registration details such as username, password, and other required information.</param>
    /// <returns>
    /// A 200 OK response with the registration result, which may include a success message or additional information about the created user.
    /// </returns>
    [HttpPost("register")]
    public async Task<IActionResult> SignUpAsync(SignUpCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    /// Authenticates a user and generates a token for accessing protected resources.
    /// </summary>
    /// <param name="command">The command containing login credentials such as username and password.</param>
    /// <returns>
    /// A 200 OK response with the authentication result, including the access token if login is successful, 
    /// or additional information if required.
    /// </returns>
    [HttpPost("login")]
    public async Task<IActionResult> SignInAsync(SignInCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    /// Initiates the password recovery process for a user who has forgotten their password.
    /// </summary>
    /// <param name="command">The command containing the user's email or identifier to start the password recovery process.</param>
    /// <returns>
    /// A 200 OK response with the result of the password recovery initiation, 
    /// such as a success message or instructions sent to the user.
    /// </returns>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    /// Logs out a user by invalidating their active session or token.
    /// </summary>
    /// <param name="command">The command containing details required for the logout process, such as user identifier or token.</param>
    /// <returns>
    /// A 200 OK response indicating the logout was successful or providing additional details about the process.
    /// </returns>
    [HttpDelete("logout")]
    public async Task<IActionResult> LogoutAsync(LogoutCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves a list of all users in the system.
    /// </summary>
    /// <returns>
    /// A 200 OK response with the list of users. If there are no users, it will return an empty list.
    /// </returns>
    [HttpGet("get-all-users")]
    public async Task<IActionResult> GetUsers()
        => Ok(await mediator.Send(new GetUserQuery()));
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StremoCloud.Application.Features.Command.Create;

namespace StremoCloud.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OtpController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Generates an OTP (One-Time Password) for a specified user or purpose.
    /// </summary>
    /// <param name="command">The command containing the required details for OTP generation.</param>
    /// <returns>
    /// A 200 OK response with the result of the OTP generation if successful; 
    /// otherwise, a 400 Bad Request response with the error message.
    /// </returns>
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateOtp([FromBody] GenerateOtpCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }
        return Ok(result);
    }

    /// <summary>
    /// Validates a provided OTP (One-Time Password) for a user or specific purpose.
    /// </summary>
    /// <param name="command">The command containing the OTP and related validation details.</param>
    /// <returns>
    /// A 200 OK response with the validation result if the OTP is valid; 
    /// otherwise, a 400 Bad Request response with the error message.
    /// </returns>
    [HttpPost("validate")]
    public async Task<IActionResult> ValidateOtp([FromBody] ValidateOtpCommand command)
    {
        var response = await mediator.Send(command);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response.Message);
    }
}

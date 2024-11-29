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

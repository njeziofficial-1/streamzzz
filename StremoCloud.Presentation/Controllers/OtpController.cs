using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StremoCloud.Application.Features.Command.Create;

namespace StremoCloud.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController(IMediator mediator) : ControllerBase
    {
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateOtp([FromBody] GenerateOtpCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(new { Otp = result });
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateOtp([FromBody] ValidateOtpCommand command)
        {
            var isValid = await mediator.Send(command);
            if (isValid)
            {
                return Ok("OTP is valid.");
            }
            return BadRequest("Invalid or expired OTP.");
        }
    }
}

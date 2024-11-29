using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StremoCloud.Application.Features.Command.Create;
using System.ComponentModel.DataAnnotations;

namespace StremoCloud.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("setUp")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ProfileSetUp([FromForm] ProfileSetupCommand command)
        {
             var result = await _mediator.Send(command);
             return result ? Ok("Profile setup successful.") : BadRequest("Profile setup failed.");
        }
    }
}

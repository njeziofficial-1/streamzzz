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

        /// <summary>
        /// Creates a new profile for a user.
        /// </summary>
        /// <param name="command">The command containing profile data to be created, sent via multipart/form-data.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <remarks>
        /// This endpoint accepts a multipart/form-data request to create a user profile.
        /// </remarks>
        [HttpPost("Create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProfile([FromForm] AddProfileCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok();
        }
    }
}

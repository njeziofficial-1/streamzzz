using MediatR;
using Microsoft.AspNetCore.Http;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Command.Update
{
    public class UpdateProfileCommand : IRequest<GenericResponse<ProfileResponse>>
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public IFormFile? Image { get; set; } // Optional: New image file
    }

}

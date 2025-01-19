using MediatR;
using Microsoft.Extensions.Configuration;
using StremoCloud.Application.Services;
using StremoCloud.Domain.Entities;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared.Helpers;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Command.Create;
public class AddProfileCommandHandler(IConfiguration configuration, IStremoUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
    : IRequestHandler<AddProfileCommand, GenericResponse<ProfileResponse>>
{
    public async Task<GenericResponse<ProfileResponse>> Handle(AddProfileCommand request, CancellationToken cancellationToken)
    {
        var response = new GenericResponse<ProfileResponse>
        {
            Message = "Error in updating user profile",
            IsSuccess = false
        };

        try
        {
            // Check if the image is provided
            string imageUrl = string.Empty;
            if (request.Image != null)
            {
                // Upload the profile image to Cloudinary
                imageUrl = await cloudinaryService.UploadImageAsync(request.Image);

                // Handle upload failure
                if (string.IsNullOrEmpty(imageUrl))
                {
                    response.Message = "Failed to upload the profile image.";
                    return response;
                }
            }

            // Fetch user by PhoneNumber
            var user = await unitOfWork.Repository<User>().FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
            if (user == null)
            {
                response.Message = "User not found.";
                return response;
            }

            // Update user's image URL
            user.ImageUrl = imageUrl;
            await unitOfWork.Repository<User>().UpdateAsync(user.Id, user);

            // Return success response
            response.IsSuccess = true;
            response.Message = "Profile updated successfully.";
            response.Data = new ProfileResponse
            {
                ImageUrl = user.ImageUrl
            };
        }
        catch (Exception ex)
        {
            // Log exception (optional: inject ILogger for logging)
            response.Message = $"An error occurred: {ex.Message}";
        }

        return response;
    }
}

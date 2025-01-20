using MediatR;
using StremoCloud.Application.Services;
using StremoCloud.Domain.Entities;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StremoCloud.Application.Features.Command.Update;

public class UpdateProfileCommandHandler(IStremoUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
    : IRequestHandler<UpdateProfileCommand, GenericResponse<ProfileResponse>>
{
    public async Task<GenericResponse<ProfileResponse>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var response = new GenericResponse<ProfileResponse>
        {
            Message = "Error in updating profile.",
            IsSuccess = false
        };

        try
        {
            // Retrieve the existing profile from the database
            var profile = await unitOfWork.Repository<Profile>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (profile == null)
            {
                response.Message = "Profile not found.";
                return response;
            }

            // Update profile details
            profile.FirstName = request.FirstName ?? profile.FirstName;
            profile.LastName = request.LastName ?? profile.LastName;

            // Check if a new image is provided
            if (request.Image != null)
            {
                // Use the existing image URL to replace the old image
                string newImageUrl = await cloudinaryService.UpdateImageAsync(request.Image, profile.ImageUrl);

                if (string.IsNullOrEmpty(newImageUrl))
                {
                    response.Message = "Failed to update the profile image.";
                    return response;
                }

                profile.ImageUrl = newImageUrl;
            }

            // Update the profile in the database
            await unitOfWork.Repository<Profile>().UpdateAsync(profile);

            // Return success response
            response.IsSuccess = true;
            response.Message = "Profile updated successfully.";
            response.Data = new ProfileResponse
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                ImageUrl = profile.ImageUrl
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


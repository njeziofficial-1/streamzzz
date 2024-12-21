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
        var response = new GenericResponse<ProfileResponse>();

        // Upload the profile image to Cloudinary
        string imageUrl = null;
        if (request.Image != null)
        {
            imageUrl = await cloudinaryService.UploadImageAsync(request.Image);
        }

        // Create new profile model
        var newProfile = new Profile
        { 
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            ImageUrl = imageUrl // Store image URL if uploaded
        };

        // Save the profile in the database
        await unitOfWork.Repository<Profile>().CreateAsync(newProfile);

        // Commit the changes to the database
        //await unitOfWork.CommitAsync();

        // Return success response with the newly created profile ID
        response.IsSuccess = true;
        response.Message = "Profile created successfully.";
        //response.Data = newProfile.ImageUrl.FileName;

        return response;
    }
}
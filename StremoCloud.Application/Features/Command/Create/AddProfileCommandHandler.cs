using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using StremoCloud.Application.Services;
using StremoCloud.Domain.Entities;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Command.Create;

public class AddProfileCommandHandler(
    IConfiguration configuration,
    IStremoUnitOfWork unitOfWork,
    ICloudinaryService cloudinaryService)
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

            // Save profile data in MongoDB
            var profile = new Profile
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                ImageUrl = imageUrl,
                PhoneNumber = request.PhoneNumber,
                CreatedOn = DateTime.Now
            };

            // Save profile data in MongoDB using the Unit of Work
            await unitOfWork.Repository<Profile>().CreateAsync(profile);

            // Return success response
            response.IsSuccess = true;
            response.Message = "Profile created successfully.";
            response.Data = new ProfileResponse
            {
                Id = profile.Id,
                ImageUrl = profile.ImageUrl,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                PhoneNumber = profile.PhoneNumber
            };
        }
        catch (Exception ex)
        {
            // Log exception (optional: inject ILogger for logging)
            response.Message = $"An error occurred: {ex.Message}";
        }

        return response;
    }

    public class AddProfileCommandValidator : AbstractValidator<AddProfileCommand>
    {
        public AddProfileCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .NotNull()
                .Matches("[A-Za-z0-9]"); // Accepts only alphabet and number
            RuleFor(p => p.LastName)
                .NotEmpty()
                .NotNull()
                .Matches("[A-Za-z0-9]");

            RuleFor(x => x.Image)
                .NotNull()
                .WithMessage("Profile image is required.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^\d+$")
                .WithMessage("Phone number must contain only numeric values.");
        }
    }


}

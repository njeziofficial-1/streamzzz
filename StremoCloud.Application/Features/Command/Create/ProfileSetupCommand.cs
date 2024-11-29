using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
namespace StremoCloud.Application.Features.Command.Create;

public record ProfileSetupCommand(
        IFormFile? Image,
        string FirstName,
        string LastName,
        string PhoneNumber
        ) : IRequest<bool>;

public class ProfileSetupCommandValidator : AbstractValidator<ProfileSetupCommand>
{
    public ProfileSetupCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{10,15}$").WithMessage("Phone number must be between 10 and 15 digits.");

        RuleFor(x => x.Image)
            .Must(BeAValidImage).When(x => x.Image != null).WithMessage("Only JPEG, PNG, or GIF files are allowed.");
    }

    private bool BeAValidImage(IFormFile? file)
    {
        if (file == null) return true;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension);
    }
}
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StremoCloud.Domain.Entities;
using StremoCloud.Infrastructure.Data;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared.Helpers;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Command.Create;

public class SignUpCommand : IRequest<GenericResponse<string>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
public class SignUpCommandHandler(IStremoUnitOfWork unitOfWork)
    : IRequestHandler<SignUpCommand, GenericResponse<string>>
{
    public async Task<GenericResponse<string>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await unitOfWork.Repository<SignUpCommand>().FirstOrDefaultAsync(x => x.Email.ToLower() == request.Email.ToLower());

        string message;
        if (existingUser != null)
        {
            message = $"User with email: {request.Email} already exists";
            return new GenericResponse<string>
            {
                Data = message,
                Message = message,
                IsSuccess = false
            };
        }

        var newUser = new SignUp
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            Password = request.Password.GenerateHash()
        };
        await unitOfWork.Repository<SignUp>().CreateAsync(newUser);

        message = "User created successfully.";
        return new GenericResponse<string>
        {
            Data = message,
            Message = message,
            IsSuccess = true
        };
    }
}

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>  
{
    public SignUpCommandValidator()
    {
        RuleFor(p => p.FirstName).NotEmpty().NotNull().Matches("[A-Za-z0-9]"); // Accepts only alphabet and number
        RuleFor(p => p.LastName).NotEmpty().NotNull().Matches("[A-Za-z0-9]");
        RuleFor(p => p.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .MaximumLength(20)
            .Matches("^[0-9]*$").WithMessage("{PhoneNumber} can only contain digits, please pass valid phone number.");//Accept only numbers from 0-9.
        RuleFor(p => p.Email)
            .NotEmpty()
            .NotNull()
            .Must(p => StringHelper.IsValidEmail(p)).WithMessage("{Email} format is incorrect, please send a valid email.");

    }
}

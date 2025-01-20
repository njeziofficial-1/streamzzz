using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using StremoCloud.Shared.Response;
namespace StremoCloud.Application.Features.Command.Create;

public record AddProfileCommand(
        IFormFile Image,
        string FirstName,
        string LastName,
        string PhoneNumber
        ) : IRequest<GenericResponse<ProfileResponse>>;

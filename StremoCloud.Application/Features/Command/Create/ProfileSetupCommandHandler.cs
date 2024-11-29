using MediatR;
using StremoCloud.Application.Services;
using StremoCloud.Domain.Entities;
namespace StremoCloud.Application.Features.Command.Create;

public class ProfileSetupCommandHandler : IRequestHandler<ProfileSetupCommand, bool>
{
    private readonly IProfileService _service;

    public ProfileSetupCommandHandler(IProfileService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(ProfileSetupCommand request, CancellationToken cancellationToken)
    {
        var profile = new Profile
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            //Image = request.Image.FileName,
        };

        return await _service.SetupProfileAsync(profile, cancellationToken);
    }
}
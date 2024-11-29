using Microsoft.AspNetCore.Http;
using StremoCloud.Domain.Entities;
namespace StremoCloud.Application.Services;

public interface IProfileService
{
    Task<bool> SetupProfileAsync(Profile profile, 
         CancellationToken cancellationToken
        );
}

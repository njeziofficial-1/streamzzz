using StremoCloud.Domain.Entities;

namespace StremoCloud.Infrastructure.Repositories;
public interface IProfileRepository
{ 
    Task AddProfileAsync(Profile profile, CancellationToken cancellationToken);
}
   
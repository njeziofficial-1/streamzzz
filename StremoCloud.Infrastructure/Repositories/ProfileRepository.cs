using MongoDB.Driver;
using StremoCloud.Domain.Entities;
using StremoCloud.Infrastructure.Data;
using static StremoCloud.Infrastructure.Constant.Constants;

namespace StremoCloud.Infrastructure.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly IMongoCollection<Profile> _profiles;

    public ProfileRepository(IDataContext dataContext)
    {
        _profiles = dataContext.GetCollection<Profile>(Collections.Profile);
    }

    /// <summary>
    /// Adds a profile to the database after validating input.
    /// </summary>
    public async Task AddProfileAsync(Profile profile, CancellationToken cancellationToken)
    {
        if (profile == null)
            throw new ArgumentNullException(nameof(profile), "Profile cannot be null.");

        // Check for duplicates by phone number
        var existingProfile = await _profiles
            .Find(p => p.PhoneNumber == profile.PhoneNumber)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingProfile != null)
            throw new InvalidOperationException($"A profile with phone number '{profile.PhoneNumber}' already exists.");

        // Add the profile to the database
        await _profiles.InsertOneAsync(profile, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Checks if a profile exists by phone number.
    /// </summary>
    public async Task<bool> ProfileExistsAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(phoneNumber))
            throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));

        var count = await _profiles
            .Find(p => p.PhoneNumber == phoneNumber)
            .CountDocumentsAsync(cancellationToken);

        return count > 0;
    }
}
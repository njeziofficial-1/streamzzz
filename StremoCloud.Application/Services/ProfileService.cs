//using CloudinaryDotNet;
//using CloudinaryDotNet.Actions;
//using Microsoft.AspNetCore.Http;
//using StremoCloud.Domain.Entities;
//using StremoCloud.Infrastructure.Repositories;
//using static StremoCloud.Infrastructure.Constant.Constants;

//namespace StremoCloud.Application.Services;

//public class ProfileService : IProfileService
//{
//    private readonly IProfileRepository _repository;
//    private readonly Cloudinary _cloudinary;

//    public ProfileService(IProfileRepository repository, Cloudinary cloudinary)
//    {
//        _repository = repository;
//        _cloudinary = cloudinary;
//    }

//    public Task<bool> CreateProfileAsync(Profile profile, CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> DeleteProfileAsync(string id, CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }

//    public async Task<bool> SetupProfileAsync(Profile profile, CancellationToken cancellationToken)
//    {
//        if (profile.Image != null)
//        {
//            using var stream = profile.Image.OpenReadStream();
//            var uploadParams = new ImageUploadParams
//            {
//                File = new FileDescription(profile.Image.FileName, stream),
//                Folder = Collections.Profile
//            };

//            var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);
//            //profile. = uploadResult.SecureUrl.ToString();
//        }

//        await _repository.AddProfileAsync(profile, cancellationToken);
//        return true;
//    }

//    public Task<bool> UpdateProfileAsync(string id, CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }
//}

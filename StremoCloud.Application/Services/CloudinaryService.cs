using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StremoCloud.Infrastructure.Options;

namespace StremoCloud.Application.Services;

public class CloudinaryService : ICloudinaryService
{
    readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> cloudinarySettings)
    {
        var account = new Account(
            cloudinarySettings.Value.CloudName,
            cloudinarySettings.Value.ApiKey,
            cloudinarySettings.Value.ApiSecret);

        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(IFormFile image)
    {
        using var stream = new MemoryStream();
        await image.CopyToAsync(stream);
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(image.FileName, stream),
            Folder = "profile_images"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult.SecureUrl.ToString();
    }
}


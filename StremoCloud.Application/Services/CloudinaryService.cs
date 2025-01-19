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
        if (cloudinarySettings == null || cloudinarySettings.Value == null)
        {
            throw new ArgumentNullException(nameof(cloudinarySettings), "Cloudinary settings cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(cloudinarySettings.Value.CloudName) ||
            string.IsNullOrWhiteSpace(cloudinarySettings.Value.ApiKey) ||
            string.IsNullOrWhiteSpace(cloudinarySettings.Value.ApiSecret))
        {
            throw new Exception("CloudinarySettings is not configured properly.");
        }

        var account = new Account(
            cloudinarySettings.Value.CloudName,
            cloudinarySettings.Value.ApiKey,
            cloudinarySettings.Value.ApiSecret);

        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(IFormFile image)
    {
        if (image == null)
        {
            throw new ArgumentException("Image cannot be null!");
        }

        using (var stream = image.OpenReadStream())
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, stream),
                Folder = "StremoCloud_ProfileSetUp"

            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Image upload failed: {uploadResult.Error?.Message}");
            }

            return uploadResult.Url.ToString();
        }
    }
}


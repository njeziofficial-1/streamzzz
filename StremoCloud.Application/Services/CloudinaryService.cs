using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StremoCloud.Infrastructure.Options;
using Microsoft.Extensions.Configuration;

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

    /// <summary>
    /// Update or replace an existing image in Cloudinary
    /// </summary>
    /// <param name="newImage">New image file</param>
    /// <param name="existingImageUrl">URL of the existing image (optional)</param>
    /// <returns>URL of the new image</returns>
    public async Task<string> UpdateImageAsync(IFormFile newImage, string existingImageUrl = null)
    {
        if (newImage == null)
        {
            throw new ArgumentException("New image cannot be null!");
        }

        // Step 1: Delete the old image if it exists
        if (!string.IsNullOrEmpty(existingImageUrl))
        {
            var publicId = ExtractPublicIdFromUrl(existingImageUrl);
            if (!string.IsNullOrEmpty(publicId))
            {
                var deleteParams = new DeletionParams(publicId);
                var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

                if (deleteResult.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception($"Failed to delete the old image: {deleteResult.Error?.Message}");
                }
            }
        }

        // Step 2: Upload the new image
        using (var stream = newImage.OpenReadStream())
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(newImage.FileName, stream),
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

    /// <summary>
    /// Extract the public ID from the Cloudinary URL
    /// </summary>
    /// <param name="imageUrl">The URL of the image</param>
    /// <returns>Public ID of the image</returns>
    private string ExtractPublicIdFromUrl(string imageUrl)
    {
        try
        {
            // Assumes the URL contains the public ID (e.g., https://res.cloudinary.com/<cloud_name>/image/upload/v12345/folder/image_name.jpg)
            var uri = new Uri(imageUrl);
            var segments = uri.AbsolutePath.Split('/');
            var folderAndFileName = string.Join('/', segments.Skip(3)); // Skip "/image/upload/"
            var publicId = folderAndFileName.Substring(0, folderAndFileName.LastIndexOf('.')); // Remove the file extension
            return publicId;
        }
        catch
        {
            return null;
        }
    }
} 

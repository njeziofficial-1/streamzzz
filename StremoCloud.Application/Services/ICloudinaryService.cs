using Microsoft.AspNetCore.Http;

namespace StremoCloud.Application.Services;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(IFormFile image);
    Task<string> UpdateImageAsync(string id, string imageUrl, string firstName, string lastname);
}

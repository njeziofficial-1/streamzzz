using Microsoft.AspNetCore.Http;

namespace StremoCloud.Shared.Response;

public class ProfileResponse
{
    public IFormFile? Image { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

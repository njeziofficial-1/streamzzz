using Microsoft.AspNetCore.Http;

namespace StremoCloud.Shared.Response;

public class ProfileResponse
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } 
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

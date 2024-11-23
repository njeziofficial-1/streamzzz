using Microsoft.AspNetCore.Http;

namespace StremoCloud.Domain.Entities;

public partial class ProfileSetup
{
    public IFormFile? Image { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber {  get; set; } = string.Empty;
}

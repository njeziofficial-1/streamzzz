using Microsoft.AspNetCore.Http;
using StremoCloud.Domain.Common;

namespace StremoCloud.Domain.Entities;

public partial class Profile : Entity
{
    public string ImageUrl { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber {  get; set; } = string.Empty;
}

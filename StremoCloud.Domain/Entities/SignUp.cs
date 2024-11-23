using StremoCloud.Domain.Common;

namespace StremoCloud.Domain.Entities;

public class SignUp : Entity
{
    public string? FirstName {  get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; } 
    public string Email { get; set; } = string.Empty;
    public string Address {  get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

using StremoCloud.Domain.Common;

namespace StremoCloud.Domain.Entities;

public class Login 
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

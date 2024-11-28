using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StremoCloud.Shared.Response;

public class UserResponse
{
    public string Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

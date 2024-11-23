using StremoCloud.Domain.Common;

namespace StremoCloud.Domain.Entities;

public class Verification : Entity
{
    public string UserId { get; set; }
    public string Token { get; set; }
}
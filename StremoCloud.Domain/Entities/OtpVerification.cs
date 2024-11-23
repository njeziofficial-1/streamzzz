using StremoCloud.Domain.Common;

namespace StremoCloud.Domain.Entities;

public partial class OtpVerification : Entity
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Otp { get; set; } = string.Empty;
    public DateTime ExpirationTime {  get; set; }
}

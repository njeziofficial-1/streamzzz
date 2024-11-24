using StremoCloud.Domain.Common;

namespace StremoCloud.Domain.Entities;

public class OtpRecord : Entity
{
        public string Otp { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; // e.g., user email or phone number
        public DateTime Expiry { get; set; }
}

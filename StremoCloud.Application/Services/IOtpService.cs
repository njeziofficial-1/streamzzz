using StremoCloud.Domain.Entities;


namespace StremoCloud.Domain.Interface;

public interface IOtpService
{
    string GenerateAndCacheOtp(string email);
    bool IsValidEmail(string email);
    bool IsOtpValid(string email, string otp);
    (bool, string) ValidateOtp(string email, string otp);
}

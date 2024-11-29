using StremoCloud.Domain.Entities;
using StremoCloud.Domain.Interface;

namespace StremoCloud.Application.Services;

public class OtpService : IOtpService
{
    private readonly Dictionary<string, OtpRecord> _otpStore = new();

    public async Task<string> GenerateOtpAsync(string email)
    {
        var otp = new Random().Next(100000, 999999).ToString();
        _otpStore[email] = new OtpRecord
        {
            Otp = otp,
            Email = email,
            Expiry = DateTime.Now.AddMinutes(5) // 5 minutes validity
        };

        // Simulate async operation
        await Task.Delay(10);

        return otp;
    }

    public bool IsValidEmail(string email)
    {
        // Basic validation: Identifier should be a valid email or phone number
        return !string.IsNullOrWhiteSpace(email) &&
               (email.Contains("@") || long.TryParse(email, out _));
    }

    public bool ValidateOtp(string email, string otp)
    {
        if (_otpStore.TryGetValue(email, out var record))
        {
            if (record.Otp == otp && record.Expiry > DateTime.UtcNow)
            {
                _otpStore.Remove(email); // Invalidate OTP after use
                return true;
            }
        }
        return false;
    }

    public bool IsOtpValid(string email, string otp)
    {
        if (_otpStore.TryGetValue(email, out var record))
        {
            return record.Otp == otp && record.Expiry > DateTime.UtcNow;
        }
        return false;
    }
}

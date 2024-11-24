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
            Expiry = DateTime.UtcNow.AddMinutes(5) // 5 minutes validity
        };

        // Simulate async operation
        await Task.Delay(10);

        return otp;
    }

    public bool IsValidIdentifier(string identifier)
    {
        // Basic validation: Identifier should be a valid email or phone number
        return !string.IsNullOrWhiteSpace(identifier) &&
               (identifier.Contains("@") || long.TryParse(identifier, out _));
    }

    public bool ValidateOtp(string identifier, string otp)
    {
        if (_otpStore.TryGetValue(identifier, out var record))
        {
            if (record.Otp == otp && record.Expiry > DateTime.UtcNow)
            {
                _otpStore.Remove(identifier); // Invalidate OTP after use
                return true;
            }
        }
        return false;
    }

    public bool IsOtpValid(string identifier, string otp)
    {
        if (_otpStore.TryGetValue(identifier, out var record))
        {
            return record.Otp == otp && record.Expiry > DateTime.UtcNow;
        }
        return false;
    }
}

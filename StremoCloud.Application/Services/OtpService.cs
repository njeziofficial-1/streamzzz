using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Caching.Memory;
using StremoCloud.Domain.Entities;
using StremoCloud.Domain.Interface;

namespace StremoCloud.Application.Services;

public class OtpService(IMemoryCache cache) : IOtpService
{
    private readonly Dictionary<string, OtpRecord> _otpStore = [];

    public string GenerateAndCacheOtp(string email)
    {
        // Generate a 6 - character OTP(numeric only)
        var otpRecord = new Random().Next(100000, 999999).ToString();
        email = email.ToLower();
        // Construct the cache key
        string cacheKey = $"Otp_{email}";

        // Cache the OTP for 5 minutes
        cache.Set(cacheKey, otpRecord, TimeSpan.FromMinutes(5));
        return otpRecord;
    }

    public bool IsValidEmail(string email)
    {
        // Basic validation: Identifier should be a valid email or phone number
        return !string.IsNullOrWhiteSpace(email) &&
               (email.Contains("@") || long.TryParse(email, out _));
    }

    public (bool, string) ValidateOtp(string email, string otp)
    {
        string message = string.Empty;
        bool isSuccess = false;
        email = email.ToLower();
        if (cache.TryGetValue(email, out string? otpRecord))
        {
            if (otpRecord != otp)
                message = $"Otp value sent {otp} is incorrect";
            else
            {
                message = "Otp validation successful";
                isSuccess = true;
            }
        }
        else
            message = "Otp is expired, please request again";
        return (isSuccess, message);
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

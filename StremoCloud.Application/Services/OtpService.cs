using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Caching.Memory;
using StremoCloud.Domain.Entities;
using StremoCloud.Domain.Interface;
using System.ComponentModel.DataAnnotations;

namespace StremoCloud.Application.Services;

public class OtpService(IMemoryCache cache) : IOtpService
{
    private readonly Dictionary<string, OtpRecord> _otpStore = [];
    string _cacheKey = "Otp_email";
    public string GenerateAndCacheOtp(string email)
    {
        // Generate a 6 - character OTP(numeric only)
        var otpRecord = new Random().Next(100000, 999999).ToString();
        // Construct the cache key
        _cacheKey = _cacheKey.Replace("email", email);

        // Cache the OTP for 5 minutes
        cache.Set(_cacheKey, otpRecord, TimeSpan.FromMinutes(5));
        return otpRecord;
    }

    public bool IsValidEmail(string email)
    {
        EmailAddressAttribute emailValidator = new();
        return emailValidator.IsValid(email);
    }

    public (bool, string) ValidateOtp(string email, string otp)
    {
        string message = string.Empty;
        bool isSuccess = false;
        email = email.Trim().ToLower();
        _cacheKey = _cacheKey.Replace("email", email);
        if (cache.TryGetValue(_cacheKey, out string? otpRecord))
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

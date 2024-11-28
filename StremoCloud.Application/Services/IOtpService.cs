using StremoCloud.Domain.Entities;


namespace StremoCloud.Domain.Interface
{
    public interface IOtpService
    {
        Task<string> GenerateOtpAsync(string email);
        bool IsValidEmail(string email);
        bool IsOtpValid(string email, string otp);
        bool ValidateOtp(string email, string otp);
    }

}

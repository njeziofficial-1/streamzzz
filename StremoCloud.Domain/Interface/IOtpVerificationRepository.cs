using StremoCloud.Domain.Entities;


namespace StremoCloud.Domain.Interface
{
    public interface IOtpVerificationRepository
    {
        Task SaveOtpAsync(OtpVerification otpVerify);
        Task<OtpVerification> GetOtpByPhoneNumberOtpAsync(string phoneNumber,
            CancellationToken cancellation);
    }
}

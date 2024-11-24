using StremoCloud.Domain.Entities;


namespace StremoCloud.Domain.Interface
{
    public interface IOtpService
    {
        Task<string> GenerateOtpAsync(string email);
        bool IsValidIdentifier(string email);
    }

}

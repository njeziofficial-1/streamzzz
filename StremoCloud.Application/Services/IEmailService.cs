namespace StremoCloud.Application.Services;

public interface IEmailService
{
    Task<bool> SendToEmailAsync(string toEmail, string message, string fullName, string subject);
}
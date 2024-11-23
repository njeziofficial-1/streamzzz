using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace StremoCloud.Application.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
    public async Task<bool> SendToEmailAsync(string toEmail, string message, string fullName, string subject)
    {
        string fromEmail = configuration["EmailSettings:FromEmail"];
        string username = configuration["EmailSettings:Username"];
        string password = configuration["EmailSettings:Password"];
        string server = configuration["EmailSettings:Server"];
        int port = Convert.ToInt32(configuration["EmailSettings:Port"]);
        try
        {
            using SmtpClient smtpClient = new SmtpClient(server, port);
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail, fullName),
                Subject = subject
            };
            mailMessage.To.Add(new MailAddress(toEmail));
            mailMessage.Body = message;
            smtpClient.Credentials = new NetworkCredential(username, password);
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception exception)
        {
            
            return false;
        }

        return true;
    }

}
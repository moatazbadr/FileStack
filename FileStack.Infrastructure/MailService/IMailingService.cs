namespace FileStack.Infrastructure.MailService;

public interface IMailingService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}

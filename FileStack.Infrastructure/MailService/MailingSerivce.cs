using FileStack.Infrastructure.MailService.MailSettings;
using MimeKit;

namespace FileStack.Infrastructure.MailService;

public class MailingSerivce : IMailingService
{
    private readonly MailSettingsHelper _mailSettings;
    public MailingSerivce(IOptions<MailSettingsHelper> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }
    public Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var Email = new MimeMessage()
        {
            Sender = MailboxAddress.Parse(_mailSettings.Mail),
            Subject = subject
        };
        Email.To.Add(MailboxAddress.Parse(toEmail));

        var builder = new BodyBuilder();

        builder.HtmlBody = body;
        Email.Body = builder.ToMessageBody();
        Email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        smtp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        smtp.Send(Email);
        smtp.Disconnect(true);
        return Task.CompletedTask;
    }
}

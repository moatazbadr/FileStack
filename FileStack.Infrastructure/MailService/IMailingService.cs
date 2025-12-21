using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStack.Infrastructure.MailService
{
    public interface IMailingService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}

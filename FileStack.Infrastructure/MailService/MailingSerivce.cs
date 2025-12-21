using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStack.Infrastructure.MailService
{
    public class MailingSerivce : IMailingService
    {
        public Task SendEmailAsync(string toEmail, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}

using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public IEmailSender GetSender(Server server)
        {
            return new EmailSender(server);
        }
    }
}
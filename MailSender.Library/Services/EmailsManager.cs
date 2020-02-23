using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services
{
    public class EmailsManager : EntityManager<Email>
    {
        public EmailsManager(IEntityStore<Email> store) : base(store)
        {
        }
    }
}

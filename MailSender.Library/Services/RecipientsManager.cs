using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services
{
    public class RecipientsManager : EntityManager<Recipient>
    {
        public RecipientsManager(IEntityStore<Recipient> store, IEntityEditor<Recipient> editor) : base(store, editor)
        {
        }
    }
}
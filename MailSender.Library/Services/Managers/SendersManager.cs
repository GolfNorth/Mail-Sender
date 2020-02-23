using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services.Managers
{
    public class SendersManager : EntityManager<Sender>
    {
        public SendersManager(IEntityStore<Sender> store, IEntityEditor<Sender> editor) : base(store, editor)
        {
        }
    }
}
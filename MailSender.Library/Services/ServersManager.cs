using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services
{
    public class ServersManager : EntityManager<Server>
    {
        public ServersManager(IEntityStore<Server> store, IEntityEditor<Server> editor) : base(store, editor)
        {
        }
    }
}
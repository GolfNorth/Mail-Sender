using System.Collections.Generic;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services
{
    public class SendersManager : IEntityManager<Sender>
    {
        private readonly IEntityStore<Sender> _store;

        public SendersManager(IEntityStore<Sender> store)
        {
            _store = store;
        }

        public IEnumerable<Sender> GetAll()
        {
            return _store.GetAll();
        }

        public void Add(Sender newSender)
        {
            _store.Add(newSender);
        }

        public void Edit(Sender sender)
        {
            _store.Edit(sender.Id, sender);
        }

        public void Remove(Sender sender)
        {
            _store.Remove(sender.Id);
        }

        public void SaveChanges()
        {
            _store.SaveChanges();
        }
    }
}
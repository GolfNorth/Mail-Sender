using System.Collections.Generic;
using System.Diagnostics;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services
{
    public class RecipientsManager : IEntityManager<Recipient>
    {
        private readonly IEntityStore<Recipient> _store;

        public RecipientsManager(IEntityStore<Recipient> store)
        {
            _store = store;
        }


        public IEnumerable<Recipient> GetAll()
        {
            return _store.GetAll();
        }

        public void Add(Recipient newRecipient)
        {
            _store.Add(newRecipient);
        }

        public void Edit(Recipient recipient)
        {
            _store.Edit(recipient.Id, recipient);
        }

        public void Remove(Recipient recipient)
        {
            _store.Remove(recipient.Id);
        }

        public void SaveChanges()
        {
            _store.SaveChanges();
        }
    }
}
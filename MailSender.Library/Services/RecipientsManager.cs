using System.Collections.Generic;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services
{
    public class RecipientsManager : IRecipientsManager
    {
        private readonly IRecipientsStore _store;

        public RecipientsManager(IRecipientsStore store)
        {
            _store = store;
        }


        public IEnumerable<Recipient> GetAll()
        {
            return _store.GetAll();
        }

        public void Add(Recipient newRecipient)
        {
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
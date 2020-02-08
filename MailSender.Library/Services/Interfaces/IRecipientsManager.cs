using System.Collections.Generic;
using MailSender.Library.Entities;

namespace MailSender.Library.Services.Interfaces
{
    public interface IRecipientsManager
    {
        IEnumerable<Recipient> GetAll();

        void Add(Recipient newRecipient);

        void Edit(Recipient recipient);

        void Delete(Recipient recipient);

        void SaveChanges();
    }
}
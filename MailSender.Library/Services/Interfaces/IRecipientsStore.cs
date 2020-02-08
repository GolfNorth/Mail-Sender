using System.Collections.Generic;
using MailSender.Library.Entities;

namespace MailSender.Library.Services.Interfaces
{
    public interface IRecipientsStore
    {
        IEnumerable<Recipient> Get();

        void Edit(int id, Recipient recipient);

        void Delete(int id);

        void SaveChanges();
    }
}
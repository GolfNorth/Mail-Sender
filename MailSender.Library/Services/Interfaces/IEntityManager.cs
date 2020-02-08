using System.Collections.Generic;

namespace MailSender.Library.Services.Interfaces
{
    public interface IEntityManager<T>
    {
        IEnumerable<T> GetAll();

        void Add(T newRecipient);

        void Edit(T recipient);

        void Remove(T recipient);

        void SaveChanges();
    }
}

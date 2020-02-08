using MailSender.Library.Data;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services.InMemory
{
    public class RecipientsStoreInMemory : DataStoreInMemory<Recipient>, IEntityStore<Recipient> //IRecipientsStore //IEntityStore<Recipient>
    {
        public RecipientsStoreInMemory() : base(DevData.Recipients)
        {
        }

        public override void Edit(int id, Recipient recipient)
        {
            var db_recipient = GetById(id);
            if (db_recipient is null) return;

            db_recipient.Name =
                recipient.Name; // Притворяемся, что мы работаем не с объектами в памяти, а с объектам в БД
            db_recipient.Address = recipient.Address;
        }
    }
}
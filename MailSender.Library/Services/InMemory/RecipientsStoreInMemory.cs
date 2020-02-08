using MailSender.Library.Data;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services.InMemory
{
    public class RecipientsStoreInMemory : EntityStoreInMemory<Recipient>, IEntityStore<Recipient>
    {
        public RecipientsStoreInMemory() : base(DevData.Recipients)
        {
        }

        public override void Edit(int id, Recipient recipient)
        {
            var dbRecipient = GetById(id);
            if (dbRecipient is null) return;

            // Притворяемся, что мы работаем не с объектами в памяти, а с объектам в БД
            dbRecipient.Name = recipient.Name;
            dbRecipient.Address = recipient.Address;
        }
    }
}
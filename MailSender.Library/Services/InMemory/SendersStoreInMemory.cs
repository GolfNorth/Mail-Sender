using MailSender.Library.Data;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services.InMemory
{
    public class SendersStoreInMemory : EntityStoreInMemory<Sender>, IEntityStore<Sender>
    {
        public SendersStoreInMemory() : base(DevData.Senders)
        {
        }

        public override void Edit(int id, Sender sender)
        {
            var dbSender = GetById(id);

            if (dbSender is null) return;

            // Притворяемся, что мы работаем не с объектами в памяти, а с объектам в БД
            dbSender.Name = sender.Name;
            dbSender.Address = sender.Address;
        }
    }
}
using System;
using MailSender.Library.Data.EntityFramework;
using MailSender.Library.Entities;

namespace MailSender.Library.Services.EntityFramework
{
    public class RecipientsStoreEntityFramework : EntityStoreEntityFramework<Recipient>
    {

        public RecipientsStoreEntityFramework(MailSenderDB db) : base(db)
        {
        }

        public override void Edit(int id, Recipient item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            var dbItem = GetById(id);
            dbItem.Name = item.Name;
            dbItem.Address = item.Address;
        }
    }
}
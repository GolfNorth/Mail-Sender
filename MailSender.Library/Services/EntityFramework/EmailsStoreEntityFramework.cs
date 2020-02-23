using System;
using MailSender.Library.Data.EntityFramework;
using MailSender.Library.Entities;

namespace MailSender.Library.Services.EntityFramework
{
    public class EmailsStoreEntityFramework : EntityStoreEntityFramework<Email>
    {
        public EmailsStoreEntityFramework(MailSenderDB db) : base(db)
        {
        }

        public override void Edit(int id, Email item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            var dbItem = GetById(id);
            dbItem.Subject = item.Subject;
            dbItem.Body = item.Body;
        }
    }
}

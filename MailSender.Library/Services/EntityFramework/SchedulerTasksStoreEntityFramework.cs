using System;
using MailSender.Library.Data.EntityFramework;
using MailSender.Library.Entities;

namespace MailSender.Library.Services.EntityFramework
{
    public class SchedulerTasksStoreEntityFramework : EntityStoreEntityFramework<SchedulerTask>
    {
        public SchedulerTasksStoreEntityFramework(MailSenderDB db) : base(db)
        {
        }

        public override void Edit(int id, SchedulerTask item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            var dbItem = GetById(id);
            dbItem.Email = item.Email;
            dbItem.Server = item.Server;
            dbItem.Sender = item.Sender;
            dbItem.Recipients = item.Recipients;
            dbItem.Time = item.Time;
        }
    }
}

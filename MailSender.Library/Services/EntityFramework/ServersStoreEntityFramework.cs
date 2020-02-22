using System;
using MailSender.Library.Data.EntityFramework;
using MailSender.Library.Entities;

namespace MailSender.Library.Services.EntityFramework
{
    public class ServersStoreEntityFramework : EntityStoreEntityFramework<Server>
    {

        public ServersStoreEntityFramework(MailSenderDB db) : base(db)
        {

        }

        public override void Edit(int id, Server item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            var dbItem = GetById(id);
            dbItem.Name = item.Name;
            dbItem.EnableSsl = item.EnableSsl;
            dbItem.Password = item.Password;
            dbItem.Port = item.Port;
            dbItem.Host = dbItem.Host;
            dbItem.Login = item.Login;
        }
    }
}

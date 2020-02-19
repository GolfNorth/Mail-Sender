using System;
using System.Collections.Generic;
using System.Linq;
using MailSender.Library.Data.EntityFramework;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services.EntityFramework
{
    public class ServersStoreEntityFramework : IEntityStore<Server>
    {
        private readonly MailSenderDB _db;

        public ServersStoreEntityFramework(MailSenderDB db)
        {
            _db = db;
        }

        public IEnumerable<Server> GetAll()
        {
            return _db.Servers.AsEnumerable();
        }

        public Server GetById(int id)
        {
            return _db.Servers.FirstOrDefault(r => r.Id == id);
        }

        public int Add(Server item)
        {
            _db.Servers.Add(item);

            SaveChanges();

            return item.Id;
        }

        public void Edit(int id, Server item)
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

            SaveChanges();
        }

        public Server Remove(int id)
        {
            var dbItem = GetById(id);

            if (dbItem is null)
                return null;

            _db.Servers.Remove(dbItem);
            SaveChanges();

            return dbItem;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}

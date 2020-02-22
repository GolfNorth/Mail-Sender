using System;
using System.Collections.Generic;
using System.Linq;
using MailSender.Library.Data.EntityFramework;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services.EntityFramework
{
    public class SendersStoreEntityFramework : IEntityStore<Sender>
    {
        private readonly MailSenderDB _db;

        public SendersStoreEntityFramework(MailSenderDB db)
        {
            _db = db;
        }

        public IEnumerable<Sender> GetAll()
        {
            return _db.Senders.AsEnumerable();
        }

        public Sender GetById(int id)
        {
            return _db.Senders.FirstOrDefault(r => r.Id == id);
        }

        public int Add(Sender item)
        {
            _db.Senders.Add(item);

            SaveChanges();

            return item.Id;
        }

        public void Edit(int id, Sender item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            var dbItem = GetById(id);
            dbItem.Name = item.Name;
            dbItem.Address = item.Address;

            SaveChanges();
        }

        public Sender Remove(int id)
        {
            var dbItem = GetById(id);

            if (dbItem is null)
                return null;

            _db.Senders.Remove(dbItem);
            SaveChanges();

            return dbItem;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}

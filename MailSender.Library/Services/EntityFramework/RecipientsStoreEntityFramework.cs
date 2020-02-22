using System;
using System.Collections.Generic;
using System.Linq;
using MailSender.Library.Data.EntityFramework;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services.EntityFramework
{
    public class RecipientsStoreEntityFramework : IEntityStore<Recipient>
    {
        private readonly MailSenderDB _db;

        public RecipientsStoreEntityFramework(MailSenderDB db)
        {
            _db = db;
        }

        public IEnumerable<Recipient> GetAll()
        {
            return _db.Recipients.AsEnumerable();
        }

        public Recipient GetById(int id)
        {
            return _db.Recipients.FirstOrDefault(r => r.Id == id);
        }

        public int Add(Recipient item)
        {
            _db.Recipients.Add(item);

            SaveChanges();

            return item.Id;
        }

        public void Edit(int id, Recipient item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            var dbItem = GetById(id);
            dbItem.Name = item.Name;
            dbItem.Address = item.Address;

            SaveChanges();
        }

        public Recipient Remove(int id)
        {
            var dbItem = GetById(id);

            if (dbItem is null)
                return null;

            _db.Recipients.Remove(dbItem);
            SaveChanges();

            return dbItem;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
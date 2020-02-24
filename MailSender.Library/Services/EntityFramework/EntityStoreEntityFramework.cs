using System.Collections.Generic;
using System.Linq;
using MailSender.Library.Data.EntityFramework;
using MailSender.Library.Entities.Base;
using MailSender.Library.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MailSender.Library.Services.EntityFramework
{
    public abstract class EntityStoreEntityFramework<T> : IEntityStore<T> where T : BaseEntity
    {
        private readonly MailSenderDB _db;
        private readonly DbSet<T> _set;

        protected EntityStoreEntityFramework(MailSenderDB db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _set.AsEnumerable();
        }

        public T GetById(int id)
        {
            return _set.Find(id);
        }

        public int Add(T item)
        {
            if (item.Id > 0) return 0;

            _set.Add(item);

            return item.Id;
        }

        public abstract void Edit(int id, T item);

        public T Remove(int id)
        {
            var dbItem = GetById(id);

            if (dbItem != null)
                _db.Entry(dbItem).State = EntityState.Deleted;

            return dbItem;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}

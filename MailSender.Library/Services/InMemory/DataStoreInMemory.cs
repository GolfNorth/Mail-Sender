using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MailSender.Library.Entities.Base;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services.InMemory
{
    public abstract class DataStoreInMemory<T> : IEntityStore<T> where T : BaseEntity
    {
        private readonly List<T> _Items;

        protected DataStoreInMemory(List<T> Items = null)
        {
            _Items = Items ?? new List<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _Items;
        }

        public T GetById(int id)
        {
            return _Items.FirstOrDefault(item => item.Id == id);
        }

        public int Create(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            if (_Items.Contains(item)) return item.Id;
            item.Id = _Items.Count == 0
                ? 1
                : _Items.Max(r => r.Id) + 1;
            _Items.Add(item);
            return item.Id;
        }

        public abstract void Edit(int id, T item);

        public T Remove(int id)
        {
            var item = GetById(id);
            if (item != null)
                _Items.Remove(item);
            return item;
        }

        public void SaveChanges()
        {
            Debug.WriteLine("Сохранение изменений в хранилище {0}.", typeof(T));
        }
    }
}
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
        private readonly List<T> _items;

        protected DataStoreInMemory(List<T> Items = null)
        {
            _items = Items ?? new List<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _items;
        }

        public T GetById(int id)
        {
            return _items.FirstOrDefault(item => item.Id == id);
        }

        public int Create(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            if (_items.Contains(item)) return item.Id;
            item.Id = _items.Count == 0
                ? 1
                : _items.Max(r => r.Id) + 1;
            _items.Add(item);
            return item.Id;
        }

        public abstract void Edit(int id, T item);

        public T Remove(int id)
        {
            var item = GetById(id);
            if (item != null)
                _items.Remove(item);
            return item;
        }

        public void SaveChanges()
        {
            Debug.WriteLine("Сохранение изменений в хранилище {0}.", typeof(T));
        }
    }
}
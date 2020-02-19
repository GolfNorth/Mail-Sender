using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MailSender.Library.Entities.Base;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services.InMemory
{
    /// <summary>
    ///     Абстрактный класс хранилища коллекции данных в памяти
    /// </summary>
    /// <typeparam name="T">Тип хранимого объекта</typeparam>
    public abstract class EntityStoreInMemory<T> : IEntityStore<T> where T : BaseEntity
    {
        /// <summary>
        ///     Коллекция данных
        /// </summary>
        private readonly List<T> _items;

        /// <summary>
        ///     Конструктор хранилища коллекции данных в памяти
        /// </summary>
        /// <param name="items">Коллекция данных</param>
        protected EntityStoreInMemory(List<T> items = null)
        {
            _items = items ?? new List<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _items;
        }

        public T GetById(int id)
        {
            return _items.FirstOrDefault(item => item.Id == id);
        }

        public int Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            if (_items.Contains(item)) return item.Id;

            item.Id = _items.Count == 0
                ? 1
                : _items.Max(r => r.Id) + 1;
            _items.Add(item);

            return item.Id;
        }

        public abstract void Edit(int id, T sender);

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
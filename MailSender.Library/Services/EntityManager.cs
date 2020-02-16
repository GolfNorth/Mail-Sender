using System.Collections.Generic;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities.Base;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services
{
    public abstract class EntityManager<T> : IEntityManager<T> where T : BaseEntity
    {
        /// <summary>
        ///     Хранилище данных
        /// </summary>
        private readonly IEntityStore<T> _store;
        /// <summary>
        ///     Редактор данных
        /// </summary>
        private readonly IEntityEditor<T> _editor;


        protected EntityManager(IEntityStore<T> store, IEntityEditor<T> editor)
        {
            _store = store;
            _editor = editor;
        }

        public IEnumerable<T> GetAll()
        {
            return _store.GetAll();
        }

        public int Add(T newItem)
        {
            return _store.Add(newItem);
        }

        public void Edit(T item)
        {
            _editor.Edit(ref item);
            _store.Edit(item.Id, item);
        }

        public void Remove(T item)
        {
            _store.Remove(item.Id);
        }

        public void SaveChanges()
        {
            _store.SaveChanges();
            _editor.Close();
        }
    }
}

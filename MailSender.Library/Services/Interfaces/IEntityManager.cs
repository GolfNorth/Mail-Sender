using System.Collections.Generic;

namespace MailSender.Library.Services.Interfaces
{
    /// <summary>
    ///     Менеджер хранилища объектов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityManager<T>
    {
        /// <summary>
        ///     Получить все объекты хранилища
        /// </summary>
        /// <returns>Перечисление объектов</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        ///     Добавить объект в хранилище
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns>Идентификатор, присвоенный хранилищем объекту</returns>
        int Add(T newItem);

        /// <summary>
        ///     Отредактировать объект в хранилище
        /// </summary>
        /// <param name="id">Идентификатор объекта, который надо отредактировать</param>
        /// <param name="item">Модель данных, которые надо передать в редактируемый объект</param>
        void Edit(T item);

        /// <summary>
        ///     Удалить объект из хранилища по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемого объекта</param>
        /// <returns>Удалённый из хранилища объект, либо <see langword="null" /> если объекта в хранилище не было</returns>
        void Remove(T item);

        /// <summary>
        ///     Сохранить сделанные изменения в хранилище
        /// </summary>
        void SaveChanges();
    }
}
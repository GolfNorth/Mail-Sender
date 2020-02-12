using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Infrastructure.Services.Interfaces
{
    /// <summary>
    ///     Интерфейс редактора объекта сущности
    /// </summary>
    /// <typeparam name="T">Тип редактируемой сущности</typeparam>
    public interface IEntityEditor<in T>
    {
        /// <summary>
        ///     Функция запуска редактирования объекта
        /// </summary>
        /// <param name="item">Редактируемый объект</param>
        void Edit(T item);
    }
}
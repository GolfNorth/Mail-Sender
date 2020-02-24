using System.Collections.Generic;
using MailSender.Library.Entities.Base;

namespace MailSender.Infrastructure.Services.Interfaces
{
    public interface IEntityExport<in T> where T : BaseEntity
    {
        /// <summary>
        ///     Экспорт данных
        /// </summary>
        /// <param name="items">Коллекция данных</param>
        void Export(IEnumerable<T> items);
    }
}
using System.Collections.Generic;
using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Класс сущности списка рассылки
    /// </summary>
    public class EmailList : BaseEntity
    {
        /// <summary>
        ///     Коллекция получателей в списке рассылки
        /// </summary>
        public ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();
    }
}
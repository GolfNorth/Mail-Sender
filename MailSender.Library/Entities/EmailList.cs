using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Класс сущности списка рассылки
    /// </summary>
    public class EmailList : NamedEntity
    {
        /// <summary>
        ///     Коллекция получателей в списке рассылки
        /// </summary>
        [Required]
        public ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();
    }
}
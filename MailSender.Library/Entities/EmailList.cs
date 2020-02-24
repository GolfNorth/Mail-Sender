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
        public virtual IEnumerable<EmailListRecipient> RecipientsList { get; set; } // = new List<Recipient>();

        public override string ToString()
        {
            var result = string.Empty;

            foreach (var r in RecipientsList)
            {
                if (result != string.Empty)
                    result += ", ";

                result += $"{r.Recipient.Name} ({r.Recipient.Address})";
            }

            return result;
        }
    }
}
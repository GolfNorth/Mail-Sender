using System.Collections.Generic;
using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    public class EmailList : BaseEntity
    {
        public ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();
    }
}
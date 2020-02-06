using System.Collections.Generic;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Электронное письмо
    /// </summary>
    public class Email
    {
        public Sender From { get; set; }

        public List<Recipient> To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
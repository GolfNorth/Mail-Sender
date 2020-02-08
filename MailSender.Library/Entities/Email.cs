using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Электронное письмо
    /// </summary>
    public class Email : BaseEntity
    {
        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
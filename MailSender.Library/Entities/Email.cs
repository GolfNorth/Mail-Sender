using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Класс сущности электронного письма
    /// </summary>
    public class Email : BaseEntity
    {
        /// <summary>
        ///     Заголовок электронного письма
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///     Тело электронного письма
        /// </summary>
        public string Body { get; set; }
    }
}
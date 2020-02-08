using System;
using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Сущность запланированного задания списка рассылки
    /// </summary>
    public class SchedulerTask : BaseEntity
    {
        /// <summary>
        ///     Время выполнения задания
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        ///     Отправитель почты в задании
        /// </summary>
        public Sender Sender { get; set; }

        /// <summary>
        ///     Список получателей писем
        /// </summary>
        public EmailList Recipients { get; set; }

        /// <summary>
        ///     Сервер, через который надо выполнить отправку почты
        /// </summary>
        public Server Server { get; set; }

        /// <summary>
        ///     Письмо, которое требуется разослать
        /// </summary>
        public Email Email { get; set; }
    }
}
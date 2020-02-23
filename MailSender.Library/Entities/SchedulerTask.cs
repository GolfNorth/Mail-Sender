using System;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public DateTime Time { get; set; }

        /// <summary>
        ///     Отправитель почты в задании
        /// </summary>
        [Required]
        public virtual Sender Sender { get; set; }

        /// <summary>
        ///     Список получателей писем
        /// </summary>
        [Required]
        public virtual EmailList Recipients { get; set; }

        /// <summary>
        ///     Сервер, через который надо выполнить отправку почты
        /// </summary>
        [Required]
        public virtual Server Server { get; set; }

        /// <summary>
        ///     Письмо, которое требуется разослать
        /// </summary>
        [Required]
        public virtual Email Email { get; set; }
    }
}
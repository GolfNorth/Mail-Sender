using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Library.Entities;

namespace MailSender.Library.Services.Interfaces
{
    public interface IEmailSender
    {
        /// <summary>
        ///     Отправить сообщение
        /// </summary>
        /// <param name="sender">Объект отправителя</param>
        /// <param name="recipient">Объект получателя</param>
        /// <param name="email">Объект электронного письма</param>
        void SendMail(Sender sender, Recipient recipient, Email email);

        /// <summary>
        ///     Отправить сообщения
        /// </summary>
        /// <param name="sender">Объект отправителя</param>
        /// <param name="recipients">Коллекция объектов получателя</param>
        /// <param name="email">Объект электронного письма</param>
        void SendMail(Sender sender, IEnumerable<Recipient> recipients, Email email);

        /// <summary>
        ///     Асинхронный метод оправки сообщений
        /// </summary>
        /// <param name="sender">Объект отправителя</param>
        /// <param name="recipient">Объект получателя</param>
        /// <param name="email">Объект электронного письма</param>
        Task SendMailAsync(Sender sender, Recipient recipient, Email email);

        /// <summary>
        ///     Асинхронный метод отправки сообщений
        /// </summary>
        /// <param name="sender">Объект отправителя</param>
        /// <param name="recipients">Коллекция объектов получателя</param>
        /// <param name="email">Объект электронного письма</param>
        Task SendMailAsync(Sender sender, EmailList recipients, Email email, CancellationToken Cancel = default);
    }
}
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Library.Entities;

namespace MailSender.Library.Services
{
    public class EmailSender
    {
        private readonly Server _server; // Объект почтового сервера

        /// <summary>
        ///     Инициализация класса отправки электронной почты
        /// </summary>
        /// <param name="server">Объект почтового сервера</param>
        public EmailSender(Server server)
        {
            _server = server;
        }

        /// <summary>
        ///     Отправить сообщение
        /// </summary>
        /// <param name="sender">Объект отправителя</param>
        /// <param name="recipient">Объект получателя</param>
        /// <param name="email">Объект электронного письма</param>
        public void SendMail(Sender sender, Recipient recipient, Email email)
        {
            using (var message = new MailMessage(sender.Address, recipient.Address, email.Subject, email.Body))
            {
                using (var client = new SmtpClient(_server.Host, _server.Port)
                {
                    EnableSsl = _server.EnableSsl,
                    Credentials = new NetworkCredential(_server.Login, _server.Password)
                })
                {
                    client.Send(message);
                }
            }
        }

        /// <summary>
        ///     Отправить сообщения
        /// </summary>
        /// <param name="sender">Объект отправителя</param>
        /// <param name="recipients">Коллекция объектов получателя</param>
        /// <param name="email">Объект электронного письма</param>
        public void SendMail(Sender sender, IEnumerable<Recipient> recipients, Email email)
        {
            foreach (var recipient in recipients)
                SendMail(sender, recipient, email);
        }

        /// <summary>
        ///     Асинхронный метод оправки сообщений
        /// </summary>
        /// <param name="sender">Объект отправителя</param>
        /// <param name="recipient">Объект получателя</param>
        /// <param name="email">Объект электронного письма</param>
        public async Task SendMailAsync(Sender sender, Recipient recipient, Email email)
        {
            using (var message = new MailMessage(sender.Address, recipient.Address, email.Subject, email.Body))
            {
                using (var client = new SmtpClient(_server.Host, _server.Port)
                {
                    EnableSsl = _server.EnableSsl,
                    Credentials = new NetworkCredential(_server.Login, _server.Password)
                })
                {
                    await client.SendMailAsync(message).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        ///     Асинхронный метод отправки сообщений
        /// </summary>
        /// <param name="sender">Объект отправителя</param>
        /// <param name="recipients">Коллекция объектов получателя</param>
        /// <param name="email">Объект электронного письма</param>
        public async Task SendMailAsync(Sender sender, EmailList recipients, Email email, CancellationToken Cancel = default)
        {
            foreach (var recipient in recipients.Recipients)
            {
                Cancel.ThrowIfCancellationRequested();
                await SendMailAsync(sender, recipient, email).ConfigureAwait(false);
            }
        }
    }
}
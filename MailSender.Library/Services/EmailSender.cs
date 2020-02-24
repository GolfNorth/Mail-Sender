using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services
{
    public class EmailSender : IEmailSender
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

        public void SendMail(Sender sender, IEnumerable<Recipient> recipients, Email email)
        {
            foreach (var recipient in recipients)
                SendMail(sender, recipient, email);
        }

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

        public async Task SendMailAsync(Sender sender, EmailList recipients, Email email,
            CancellationToken Cancel = default)
        {
            foreach (var recipientsList in recipients.RecipientsList)
            {
                Cancel.ThrowIfCancellationRequested();
                await SendMailAsync(sender, recipientsList.Recipient, email).ConfigureAwait(false);
            }
        }
    }
}
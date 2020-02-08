using System.Net;
using System.Net.Mail;

namespace MailSender.Library.Services
{
    public class EmailSend
    {
        private readonly string _host; // Адрес почтового сервера
        private readonly string _login; // Логин для авторизации на почтовом сервере
        private readonly string _password; // Пароль для авторизации на почтовом сервере
        private readonly int _port; // Порт почтового сервера
        private readonly bool _ssl; // Защита соединения

        /// <summary>
        ///     Инициализация класса отправки электронной почты
        /// </summary>
        /// <param name="host">Адрес почтового сервера</param>
        /// <param name="port">Порт почтового сервера</param>
        /// <param name="login">Логин для авторизации на почтовом сервере</param>
        /// <param name="password">Пароль для авторизации на почтовом сервере</param>
        /// <param name="ssl">Защита соединения</param>
        public EmailSend(string host, int port, string login, string password, bool ssl = true)
        {
            _host = host;
            _port = port;
            _login = login;
            _password = password;
            _ssl = ssl;
        }

        /// <summary>
        ///     Отправить сообщение
        /// </summary>
        /// <param name="from">Отправитель</param>
        /// <param name="to">Получатель</param>
        /// <param name="subject">Тема</param>
        /// <param name="body">Сообщение</param>
        public void SendMail(string from, string to, string subject, string body)
        {
            using (var message = new MailMessage(from, to, subject, body))
            {
                using (var client = new SmtpClient(_host, _port)
                {
                    EnableSsl = _ssl,
                    Credentials = new NetworkCredential(_login, _password)
                })
                {
                    client.Send(message);
                }
            }
        }
    }
}
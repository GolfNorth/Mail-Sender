using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Почтовый сервер
    /// </summary>
    public class Server : NamedEntity
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; } = true;

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
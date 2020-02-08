using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Сущность сервера отправки электронной почты
    /// </summary>
    public class Server : NamedEntity
    {
        /// <summary>
        ///     IP-адрес и имя хоста сервера
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        ///     Порт сервера
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     Использовать SSL шифрование
        /// </summary>
        public bool EnableSsl { get; set; } = true;

        /// <summary>
        ///     Логин для авторизации на сервере
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        ///     Пароль для авторизации на сервере
        /// </summary>
        public string Password { get; set; }
    }
}
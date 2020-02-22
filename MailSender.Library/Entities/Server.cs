using System;
using System.ComponentModel.DataAnnotations;
using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Сущность сервера отправки электронной почты
    /// </summary>
    public class Server : NamedEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public override string Name
        {
            get => base.Name;
            set
            {
                if (value is null) throw new ArgumentException("Пустая ссылка на имя", nameof(value));
                if (value.Length <= 2) throw new ArgumentException("Имя должно быть содержать два или более символов", nameof(value));
                if (value.Length > 20) throw new ArgumentException("Имя должно быть содержать не более 20 символов", nameof(value));

                base.Name = value;
            }
        }

        /// <summary>
        ///     IP-адрес и имя хоста сервера
        /// </summary>
        [Required]
        public string Host { get; set; }

        /// <summary>
        ///     Порт сервера
        /// </summary>
        [Required]
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
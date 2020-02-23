using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Сущность сервера отправки электронной почты
    /// </summary>
    public class Server : NamedEntity, IDataErrorInfo
    {
        private const int MinNameLength = 2;
        private const int MaxNameLength = 50;

        [Required, MinLength(MinNameLength), MaxLength(MaxNameLength)]
        public override string Name { get; set; }

        /// <summary>
        ///     IP-адрес или имя хоста сервера
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

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case nameof(Name):
                        if (Name is null)
                        {
                            result = "Введите имя";
                            break;
                        }

                        if (Name.Length <= 2) result = $"Имя должно быть содержать {MinNameLength} или более символов";
                        if (Name.Length >= 20) result = $"Имя должно быть содержать не более {MaxNameLength} символов";

                        break;
                    case nameof(Host):
                        if (Host is null)
                        {
                            result = "Введите IP-адрес или имя хоста сервера";
                            break;
                        }

                        var regex = new Regex(
                            @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$");

                        if (!regex.IsMatch(Host)) result = "Неправильный формат IP-адреса или имени хоста сервера";

                        break;
                }

                return result;
            }
        }
    }
}
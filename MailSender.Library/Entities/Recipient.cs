using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Сущность получателя списка рассылки
    /// </summary>
    public class Recipient : PersonEntity, IDataErrorInfo
    {
        private const int MinNameLength = 2;
        private const int MaxNameLength = 50;

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public override string Name { get; set; }

        [Required] public override string Address { get; set; }

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
                    case nameof(Address):
                        if (Address is null)
                        {
                            result = "Введите Email";
                            break;
                        }

                        var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

                        if (!regex.IsMatch(Address)) result = "Неправильный формат EMail";

                        break;
                }

                return result;
            }
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Сущность получателя списка рассылки
    /// </summary>
    public class Recipient : PersonEntity
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

        [Required]
        public override string Address
        {
            get => base.Address;
            set
            {
                var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

                if (!regex.IsMatch(value)) throw new ArgumentException("Неправильный формат EMail", nameof(value));

                base.Address = value;
            }
        }
    }
}
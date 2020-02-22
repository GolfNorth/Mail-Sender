using System.ComponentModel.DataAnnotations;
using MailSender.Library.Entities.Base;

namespace MailSender.Library.Entities
{
    /// <summary>
    ///     Класс сущности электронного письма
    /// </summary>
    public class Email : BaseEntity
    {
        private const int MinSubjectLength = 2;

        /// <summary>
        ///     Заголовок электронного письма
        /// </summary>
        [Required]
        [MinLength(MinSubjectLength)]
        public string Subject { get; set; }

        /// <summary>
        ///     Тело электронного письма
        /// </summary>
        [Required]
        public string Body { get; set; }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case nameof(Subject):
                        if (Subject is null)
                        {
                            result = "Введите заголовок";
                            break;
                        }

                        if (Subject.Length <= 2) result = $"Заголовок должен содержать {MinSubjectLength} или более символов";

                        break;
                    case nameof(Body):
                        if (Body is null)
                            result = "Введите тело письма";

                        break;
                }

                return result;
            }
        }
    }
}
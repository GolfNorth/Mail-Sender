using System.ComponentModel.DataAnnotations;

namespace MailSender.Library.Entities.Base
{
    /// <summary>
    ///     Абстрактный класс базовой сущности
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        ///     Идентификатор сущности
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
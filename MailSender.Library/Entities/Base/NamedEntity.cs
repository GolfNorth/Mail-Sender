namespace MailSender.Library.Entities.Base
{
    /// <summary>
    ///     Абстрактный класс именованной сущности
    /// </summary>
    public abstract class NamedEntity : BaseEntity
    {
        /// <summary>
        ///     Имя сущности
        /// </summary>
        public string Name { get; set; }
    }
}
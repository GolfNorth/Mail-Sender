namespace MailSender.Library.Entities.Base
{
    /// <summary>
    ///     Абстрактный класс пернсональной сущности
    /// </summary>
    public abstract class PersonEntity : NamedEntity
    {
        /// <summary>
        ///     Электронный адресс сущности
        /// </summary>
        public virtual string Address { get; set; }
    }
}
namespace MailSender.Library.Entities.Base
{
    public abstract class NamedEntity : BaseEntity
    {
        public string Name { get; set; }
    }
}
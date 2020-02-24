namespace MailSender.Library.Entities
{
    public class EmailListRecipient
    {
        public int EmailListId { get; set; }
        public virtual EmailList EmailList { get; set; }

        public int RecipientId { get; set; }
        public virtual Recipient Recipient { get; set; }
    }
}

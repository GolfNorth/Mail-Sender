namespace MailSender.Library.Entities
{
    public class SchedulerTaskRecipient
    {
        public int SchedulerTaskId { get; set; }
        public virtual SchedulerTask SchedulerTask { get; set; }

        public int RecipientId { get; set; }
        public virtual Recipient Recipient { get; set; }
    }
}
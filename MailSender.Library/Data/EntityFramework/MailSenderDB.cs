using MailSender.Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace MailSender.Library.Data.EntityFramework
{
    public class MailSenderDB : DbContext
    {
        public MailSenderDB(DbContextOptions opt) : base(opt)
        {
        }

        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailList> EmailLists { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<SchedulerTask> SchedulerTasks { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Server> Servers { get; set; }
    }
}
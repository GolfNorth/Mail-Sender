using System;
using MailSender.Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MailSender.Library.Data.EntityFramework
{
    public class MailSenderDB : DbContext
    {
        public MailSenderDB() : base() { }

        public MailSenderDB(DbContextOptions opt) : base(opt) { }

        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailList> EmailLists { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<SchedulerTask> SchedulerTasks { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Server> Servers { get; set; }

        // Побеждаем проблему с миграцией
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
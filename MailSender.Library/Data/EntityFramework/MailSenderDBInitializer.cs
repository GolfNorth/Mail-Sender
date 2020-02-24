using System;
using System.Linq;
using System.Threading.Tasks;
using MailSender.Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace MailSender.Library.Data.EntityFramework
{
    public class MailSenderDBInitializer
    {
        private readonly MailSenderDB _db;

        public MailSenderDBInitializer(MailSenderDB db) => _db = db;

        public async Task InitializeAsync()
        {
            _db.Database.Migrate();

#if DEBUG
            await SeedAsync(_db.Emails).ConfigureAwait(false);
            await SeedAsync(_db.Servers);
            await SeedAsync(_db.Senders);
            await SeedAsync(_db.Recipients);

            
            if (!await _db.EmailLists.AnyAsync())
            {
                var newEmailList = new EmailList()
                {
                    Name = "New list"
                };
                newEmailList.RecipientsList = await _db.Recipients.OrderBy(r => r.Id).Take(5)
                    .Select(recipient => new EmailListRecipient {Recipient = recipient, EmailList = newEmailList})
                    .ToArrayAsync();

                _db.EmailLists.Add(newEmailList);
                await _db.SaveChangesAsync();
            }

            if (!await _db.SchedulerTasks.AnyAsync())
            {
                _db.SchedulerTasks.Add(new SchedulerTask
                {
                    Time = DateTime.Now.Add(TimeSpan.FromDays(10)),
                    Server = await _db.Servers.FirstOrDefaultAsync(),
                    Recipients = await _db.EmailLists.FirstOrDefaultAsync(),
                    Email = await _db.Emails.FirstOrDefaultAsync(),
                    Sender = await _db.Senders.FirstOrDefaultAsync()
                });

                await _db.SaveChangesAsync();
            }
#endif
        }

#if DEBUG
        private async Task SeedAsync(DbSet<Email> emails)
        {
            if (await emails.AnyAsync().ConfigureAwait(false)) return;

            for (var i = 0; i < 10; i++)
                emails.Add(new Email { Subject = $"Письмо {i}", Body = $"Текст письма {i}" });

            await _db.SaveChangesAsync();
        }

        private async Task SeedAsync(DbSet<Server> servers)
        {
            if (await servers.AnyAsync()) return;

            servers.Add(new Server
            {
                Name = "Localhost",
                Host = "localhost",
                Port = 25,
                EnableSsl = false
            });

            /*
            for (var i = 0; i < 10; i++)
                servers.Add(new Server { Name = $"Сервер {i}", Host = $"smtp.server{i}.ru", Login = "login", Password = "pass" });
            */
            
            await _db.SaveChangesAsync();
        }

        private async Task SeedAsync(DbSet<Sender> senders)
        {
            if (await senders.AnyAsync()) return;

            for (var i = 0; i < 10; i++)
                senders.Add(new Sender { Name = $"Отправитель {i}", Address = $"sender{i}@server.ru" });
            
            await _db.SaveChangesAsync();
        }

        private async Task SeedAsync(DbSet<Recipient> recipients)
        {
            if (await recipients.AnyAsync()) return;

            for (var i = 0; i < 10; i++)
                recipients.Add(new Recipient { Name = $"Получатель {i}", Address = $"recipient{i}@server.ru" });
            
            await _db.SaveChangesAsync();
        }
#endif
    }
}

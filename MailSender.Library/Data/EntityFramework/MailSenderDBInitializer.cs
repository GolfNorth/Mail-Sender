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
        }
    }
}

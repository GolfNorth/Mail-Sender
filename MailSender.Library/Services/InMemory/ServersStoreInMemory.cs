using MailSender.Library.Data;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services.InMemory
{
    public class ServersStoreInMemory : EntityStoreInMemory<Server>, IEntityStore<Server>
    {
        public ServersStoreInMemory() : base(DevData.Servers)
        {
        }

        public override void Edit(int id, Server server)
        {
            var dbServer = GetById(id);
            if (dbServer is null) return;

            // Притворяемся, что мы работаем не с объектами в памяти, а с объектам в БД
            dbServer.Name = server.Name;
            dbServer.Host = server.Host;
            dbServer.Port = server.Port;
            dbServer.EnableSsl = server.EnableSsl;
            dbServer.Login = server.Login;
            dbServer.Password = server.Password;
        }
    }
}
using System;
using System.Collections.Generic;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services
{
    public class ServersManager : IEntityManager<Server>
    {
        private readonly IEntityStore<Server> _store;

        public ServersManager(IEntityStore<Server> store)
        {
            _store = store;
        }


        public IEnumerable<Server> GetAll()
        {
            return _store.GetAll();
        }

        public void Add(Server newServer)
        {
            throw new NotImplementedException();
        }

        public void Edit(Server server)
        {
            _store.Edit(server.Id, server);
        }

        public void Remove(Server server)
        {
            _store.Remove(server.Id);
        }

        public void SaveChanges()
        {
            _store.SaveChanges();
        }
    }
}
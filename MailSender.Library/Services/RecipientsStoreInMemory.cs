using System.Collections.Generic;
using MailSender.Library.Data;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services
{
    public class RecipientsStoreInMemory : IRecipientsStore
    {
        public IEnumerable<Recipient> Get()
        {
            return DevData.Recipients;
        }

        public void Edit(int id, Recipient recipient)
        {
            // Так как это хранилище данных в памяти, то здесь ничего не делаем
        }

        public void Delete(int id)
        {
            // Так как это хранилище данных в памяти, то здесь ничего не делаем
        }

        public void SaveChanges()
        {
            // Так как это хранилище данных в памяти, то здесь ничего не делаем
        }
    }
}
using System;
using MailSender.Library.Entities;
using MailSender.Library.Services.InMemory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.Library.Tests.Services
{
    [TestClass]
    public class RecipientsStoreInMemoryTests
    {
        [TestMethod]
        public void Create_throw_ArgumentNullException_if_items_is_null()
        {
            var store = new RecipientsStoreInMemory();

            Assert.ThrowsException<ArgumentNullException>(() => store.Create(null));
        }

        [TestMethod]
        public void Remove_return_null_if_id_is_0()
        {
            var store = new RecipientsStoreInMemory();

            Assert.IsNull(store.Remove(0));
        }

        [TestMethod]
        public void Create_return_id_more_than_0()
        {
            var store = new RecipientsStoreInMemory();

            Assert.IsTrue(store.Create(new Recipient()) > 0);
        }
    }
}

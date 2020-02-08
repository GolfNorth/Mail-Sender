using System;
using MailSender.Library.Services.InMemory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.Library.Tests.Services
{
    [TestClass]
    public class ServersStoreInMemoryTests
    {
        [TestMethod]
        public void Create_throw_ArgumentNullException_if_items_is_null()
        {
            var store = new ServersStoreInMemory();

            Assert.ThrowsException<ArgumentNullException>(() => store.Create(null));
        }
    }
}

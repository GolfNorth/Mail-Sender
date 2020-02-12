using MailSender.Library.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.Library.Tests.Service
{
    [TestClass]
    public class TestEncoderTests
    {
        [TestMethod]
        public void Encode_ABC_to_BCD_with_key_1()
        {
            const string str = "ABC";
            const int key = 1;
            const string expectedStr = "BCD";

            var actualStr = PasswordEncoder.Encode(str, key);

            Assert.AreEqual(expectedStr, actualStr);
        }

        [TestMethod]
        public void Decode_BCD_to_ABC_with_key_1()
        {

            const string str = "BCD";
            const int key = 1;
            const string expectedStr = "ABC";

            var actualStr = PasswordEncoder.Decode(str, key);

            Assert.AreEqual(expectedStr, actualStr);
        }
    }
}
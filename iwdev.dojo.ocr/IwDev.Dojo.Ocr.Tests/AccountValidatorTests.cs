using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IwDev.Dojo.Ocr.Tests
{
    public class AccountValidatorTests
    {
        [TestMethod]
        public void IsValid()
        {
            var target = new AccountValidator();
            var number = "457508000";
            var actual = target.IsValid(number);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void IsNotValid()
        {
            var target = new AccountValidator();
            var number = "457508001";
            var actual = target.IsValid(number);
            Assert.AreEqual(true, actual);
        }
    }
}
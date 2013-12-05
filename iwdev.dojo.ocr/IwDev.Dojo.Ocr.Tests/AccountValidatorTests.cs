using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IwDev.Dojo.Ocr.Tests
{
    [TestClass]
    public class AccountValidatorTests
    {
        [TestMethod]
        public void IsValid51()
        {
            var target = new AccountValidator();
            var number = "000000051";
            var actual = target.IsValid(number);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void IsNotValid51WrongLength()
        {
            var target = new AccountValidator();
            var number = "00000051";
            var actual = target.IsValid(number);
            Assert.AreEqual(false, actual);
        }

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
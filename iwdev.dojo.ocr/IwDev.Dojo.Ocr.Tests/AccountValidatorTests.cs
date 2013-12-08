using NUnit.Framework;

namespace IwDev.Dojo.Ocr.Tests
{
    [TestFixture]
    public class AccountValidatorTests
    {
        [Test]
        public void IsValid51()
        {
            var target = new AccountValidator();
            var number = "000000051";
            var actual = target.IsValid(number);
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void IsNotValid51WrongLength()
        {
            var target = new AccountValidator();
            var number = "00000051";
            var actual = target.IsValid(number);
            Assert.AreEqual(false, actual);
        }

        [Test]
        public void IsValid()
        {
            var target = new AccountValidator();
            var number = "457508000";
            var actual = target.IsValid(number);
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void IsNotValid()
        {
            var target = new AccountValidator();
            var number = "457508001";
            var actual = target.IsValid(number);
            Assert.AreEqual(false, actual);
        }

        [Test]
        public void FastValid()
        {
            var target = new AccountValidator();
            var number = new[] { 4,5,7,5,0,8,0,0,0 };
            var actual = target.IsValid(number);
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void FastInvaild()
        {
            var target = new AccountValidator();
            var number = new[] { 8, 8, 8, 8, 8, 7, 8, 8, 8 };
            var actual = target.IsValid(number);
            Assert.AreEqual(false, actual);
        }
    }
}
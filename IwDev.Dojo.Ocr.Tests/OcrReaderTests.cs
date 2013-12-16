using System.IO;
using NUnit.Framework;

namespace IwDev.Dojo.Ocr.Tests
{
    [TestFixture]
    public class OcrReaderTests
    {
        protected OcrReader Target { get; set; }

        [TestFixtureSetUp]
        public void Setup()
        {
            Target = new OcrReader(new OcrGuesser(), new AccountValidator());
        }

        [Test]
        public void ZeroToNineOneLine()
        {
            //var lines = File.ReadAllLines("testFile.txt");
            var lines = new[]
                {
                    " _     _  _     _  _  _  _  _  ",
                    "| |  | _| _||_||_ |_   ||_||_| ",
                    "|_|  ||_  _|  | _||_|  ||_| _| ",
                    "",
                    " _  _  _  _  _  _  _  _  _ ",
                    "| || || || || || || || || |",
                    "|_||_||_||_||_||_||_||_||_|",
                    ""
                };
            var numbers = Target.LinesToNumbers(lines);

            Assert.AreEqual("0123456789", numbers[0]);
            Assert.AreEqual("000000000", numbers[1]);
        }

        [Test]
        public void WithSomeErrors()
        {
            var lines = new[]
                {
                    " _     _  _     _  _  _  _  _  ",
                    "| |  | _| _||_||_ |_   ||_||_| ",
                    "|_|  | _  _|  | _||_|  ||_| _| ",
                    "",
                    " _  _  _  _  _  _  _  _  _ ",
                    "  || || || || || || || || |",
                    "|_||_||_||_||_||_||_||_||_|",
                    ""
                };
            var numbers = Target.LinesToNumbers(lines);

            Assert.AreEqual("0123456789", numbers[0]);
            Assert.AreEqual("000000000", numbers[1]);
        }

        [Test]
        public void WithTooManyErrors()
        {
            var lines = new[]
                {
                    " _     _  _     _  _  _  _  _  ",
                    "| |  | _| _||_||_ |_   ||_||_| ",
                    "|_|  |    _|  | _||_|  ||_| _| ",
                    "",
                    " _  _  _  _  _  _  _  _  _ ",
                    "   | || || || || || || || |",
                    "|_||_||_||_||_||_||_||_||_|",
                    ""
                };
            var numbers = Target.LinesToNumbers(lines);

            Assert.AreEqual("01?3456789", numbers[0]);
            Assert.AreEqual("?00000000", numbers[1]);
        }

        [Test]
        public void GoodVersion()
        {
            var lines = new[]
                {
                 " _  _  _  _  _  _  _  _  _ ",
                 "|_||_||_||_||_||_||_||_||_|",
                 "|_||_||_||_||_||_||_||_||_|",
                    ""
                };
            var result = Target.LinesToAccountNumbers(lines);

            Assert.AreEqual("888888888", result[0].AccountNumber);
            Assert.AreEqual(false, result[0].AccountNumberIsValid);
            Assert.AreEqual(true, result[0].AccountNumberOptions.Contains("888886888"));
            Assert.AreEqual(true, result[0].AccountNumberOptions.Contains("888888880"));
            Assert.AreEqual(true, result[0].AccountNumberOptions.Contains("888888988"));
        }

        [Test]
        public void UserCase4Part1()
        {
            var lines = new[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                    ""
                };
            var result = Target.LinesToAccountNumbers(lines);

            Assert.AreEqual("111111111", result[0].AccountNumber);
            Assert.AreEqual(false, result[0].AccountNumberIsValid);
            Assert.AreEqual(true, result[0].AccountNumberOptions.Contains("711111111"));
        }

        [Test]
        public void UserCase4()
        {
            var lines = File.ReadAllLines("UseCase4.txt");
            var result = Target.LinesToAccountNumbers(lines);

            int i = 0;

            Assert.AreEqual("711111111", result[i].Display);
            Assert.AreEqual("777777177", result[++i].Display);
            Assert.AreEqual("200800000", result[++i].Display);
            Assert.AreEqual("333393333", result[++i].Display);
            Assert.AreEqual("888888888 AMB ['888886888', '888888880', '888888988']", result[++i].Display);
            Assert.AreEqual("555555555 AMB ['555655555', '559555555']", result[++i].Display);
            Assert.AreEqual("666666666 AMB ['666566666', '686666666']", result[++i].Display);
            Assert.AreEqual("999999999 AMB ['899999999', '993999999', '999959999']", result[++i].Display);
            Assert.AreEqual("490067715 AMB ['490067115', '490067719', '490867715']", result[++i].Display);

            i = i + 1;
            //Assert.AreEqual("123456789", result[++i].Display);

            Assert.AreEqual("000000051", result[++i].Display);
            Assert.AreEqual("490867715", result[++i].Display);
        }
    }
}
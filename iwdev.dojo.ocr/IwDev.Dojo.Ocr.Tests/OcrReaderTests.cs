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

    }
}
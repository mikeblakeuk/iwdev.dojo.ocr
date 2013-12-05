using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IwDev.Dojo.Ocr.Tests
{
    [TestClass]
    public class OcrReaderTests
    {
        protected OcrReader Target { get; set; }
        
        [TestInitialize]
        public void Setup()
        {
            Target = new OcrReader(new OcrGuesser(), new AccountValidator());
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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
    }
}
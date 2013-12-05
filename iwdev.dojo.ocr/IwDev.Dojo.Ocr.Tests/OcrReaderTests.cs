using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IwDev.Dojo.Ocr.Tests
{
    [TestClass]
    public class OcrReaderTests
    {
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
            var numbers = new OcrReader().LinesToNumbers(lines);

            Assert.AreEqual("0123456789", numbers[0]);
            Assert.AreEqual("000000000", numbers[1]);
        }

        [TestMethod]
        public void WithErrors()
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
            var numbers = new OcrReader().LinesToNumbers(lines);

            Assert.AreEqual("01?3456789", numbers[0]);
            Assert.AreEqual("?00000000", numbers[1]);
        }
    }
}
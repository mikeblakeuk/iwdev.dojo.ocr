using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IwDev.Dojo.Ocr.Tests
{
    [TestClass]
    public class OcrGuesserTests
    {
        [TestMethod]
        public void FixChar()
        {
            var data = " _ " +
                       "| |" +
                       " _|";
            var e = new OcrGuesser().Guesser(data);

            Assert.IsTrue(e.Contains(0));
            Assert.IsTrue(e.Contains(9));
        }

        [TestMethod]
        public void IgnoreCorners()
        {
            var data = "__ " +
                       "| |" +
                       "|_|";
            var e = new OcrGuesser().Guesser(data);

            Assert.IsTrue(e.Contains(0));
        }
    }
}
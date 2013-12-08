using System.Linq;
using NUnit.Framework;

namespace IwDev.Dojo.Ocr.Tests
{
    [TestFixture]
    public class OcrGuesserTests
    {
        [Test]
        public void FixChar()
        {
            var data = " _ " +
                       "| |" +
                       " _|";
            var e = new OcrGuesser().Guesser(data);

            Assert.IsTrue(e.Contains(0));
            Assert.IsTrue(e.Contains(9));
        }

        [Test]
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
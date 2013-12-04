using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IwDev.Dojo.Ocr.Tests
{
    [TestClass]
    public class LinesToCharactersTests
    {
        [TestMethod]
        public void WhenTheBlockIsAZeroReturnAZeroCharacter()
        {
            var block = " _ | ||_|";

            var e = new BlockToCharsEngine();

            var actual = e.ConvertBlockToChars(block);

            var expected = "0";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenGivenTwoBlocksSplitIntoTwoChars()
        {
            /*         var testData =
                         "AAA BBB" +
                         "AAA BBB"+
                         "AAA BBB";
                     */

            var realData =
                "     _ " +
                "  |  _|" +
                "  | |_";

            var e = new BlockToCharsEngine();

            var actual = e.ConvertLongLineToBlocks(realData);

            Assert.AreEqual(2, actual.Count);
        }

        [TestMethod]
        public void WhenGivenTwoBlocksSplitIntoTwoCharsAndEachIsCorrect()
        {
            /*         var testData =
                         "AAA BBB" +
                         "AAA BBB"+
                         "AAA BBB";
                     */

            const string realData = "    _ " +
                                    "  | _|" +
                                    "  ||_";

            var e = new BlockToCharsEngine();

            var bucket = e.ConvertLongLineToBlocks(realData);

            Assert.AreEqual(
                "   " +
                "  |" +
                "  |", bucket[0]);
        }

        [TestMethod]
        public void TestTwo()
        {
            var testData =
                 "AAABBBCCC" +
                 "AAABBBCCC" +
                 "AAABBBCCC";

            var e = new BlockToCharsEngine();

            var actual = e.ConvertLongLineToBlocks(testData);

            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual("AAAAAAAAA", actual[0]);
            Assert.AreEqual("BBBBBBBBB", actual[1]);
            Assert.AreEqual("CCCCCCCCC", actual[2]);
        }



        [TestMethod]
        public void TestOne()
        {
            var testData =
                 "AAA" +
                 "AAA" +
                 "AAA";

            var e = new BlockToCharsEngine();

            var actual = e.ConvertLongLineToBlocks(testData);

            Assert.AreEqual(1, actual.Count);

            Assert.AreEqual("AAAAAAAAA", actual[0]);
        }


        //[TestMethod]
        public void TestThree()
        {
            var testData =
                 "AAA" + Environment.NewLine +
                 "AAA" + Environment.NewLine +
                 "AAA" + Environment.NewLine +
                 "BBBCCC" + Environment.NewLine +
                 "BBBCCC" + Environment.NewLine +
                 "BBBCCC" + Environment.NewLine;

            var e = new BlockToCharsEngine();

            var actual = e.ConvertLongLineToBlocks(testData);

            Assert.AreEqual(2, actual.Count);
        }
    }
}

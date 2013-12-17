using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace IwDev.Dojo.Ocr.Tests
{
    [TestFixture]
    class FileGenerator
    {
        [Test]
        [Ignore]
        public void WriteTestFile()
        {
            var r = new Random();

            var sb = new StringBuilder();

            var guesser = new OcrGuesser();

            for (int i = 0; i < 5000; i++)
            {
                var account = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    account[j] = r.Next(0, 9);
                }
                var lines = guesser.UnGuesser(account);
                sb.AppendLine(lines);
            }
            
            File.WriteAllText("Sample.txt", sb.ToString());
        }
    }
}

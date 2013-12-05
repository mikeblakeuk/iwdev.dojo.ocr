using System;
using System.Linq;
using System.Collections.Generic;

namespace IwDev.Dojo.Ocr.Tests
{
    public class OcrReader
    {
        // Pass this in?
        private static readonly IDictionary<int, string> Blocks = new Dictionary<int, string>(10);

        static OcrReader()
        {
            Blocks.Add(0, " _ " +
                          "| |" +
                          "|_|");
            Blocks.Add(1, "   " +
                          "  |" +
                          "  |");
            Blocks.Add(2, " _ " +
                          " _|" +
                          "|_ ");
            Blocks.Add(3, " _ " +
                          " _|" +
                          " _|");
            Blocks.Add(4, "   " +
                          "|_|" +
                          "  |");
            Blocks.Add(5, " _ " +
                          "|_ " +
                          " _|");
            Blocks.Add(6, " _ " +
                          "|_ " +
                          "|_|");
            Blocks.Add(7, " _ " +
                          "  |" +
                          "  |");
            Blocks.Add(8, " _ " +
                          "|_|" +
                          "|_|");
            Blocks.Add(9, " _ " +
                          "|_|" +
                          " _|");
        }

        public string[] LinesToNumbers(string[] lines)
        {
            var numbers = new List<string>();
            // Check Line count is correct mod 4
            for (var i = 0; i < lines.Length - 3; i += 4)
            {
                // Check lines[i +3] == string.Empty;
                numbers.Add(ThreeLines(lines[i], lines[i + 1], lines[i + 2]));
            }
            return numbers.ToArray();
        }

        private static string ThreeLines(string one, string two, string three)
        {
            var numbers = new List<string>();
            // Trim if too long?
            // Check blocks of 3
            // Check all the same length
            for (var i = 0; i < one.Length - 2; i += 3)
            {
                numbers.Add(ThreeChars(one.Substring(i, 3), two.Substring(i, 3), three.Substring(i, 3)));
            }
            return String.Join(String.Empty, numbers);
        }

        private static string ThreeChars(string first, string second, string thrid)
        {
            var block = first + second + thrid;
            var known = Blocks.FirstOrDefault(x => x.Value == block);
            if (known.Value == null)
                return "?";
            // add 'guessing logic'
            return known.Key.ToString();
        }
    }
}

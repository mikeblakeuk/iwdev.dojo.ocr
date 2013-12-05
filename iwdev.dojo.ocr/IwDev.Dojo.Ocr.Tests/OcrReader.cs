using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace IwDev.Dojo.Ocr.Tests
{
    public class OcrReader
    {
        private readonly OcrGuesser _guesser;
        private readonly AccountValidator _validator;

        public OcrReader(OcrGuesser guesser, AccountValidator validator)
        {
            _guesser = guesser;
            _validator = validator;
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

        private string ThreeLines(string one, string two, string three)
        {
            var numbers = new List<int[]>();
            // Trim if too long?
            // Check blocks of 3
            // Check all the same length
            for (var i = 0; i < one.Length - 2; i += 3)
            {
                numbers.Add(ThreeChars(one.Substring(i, 3), two.Substring(i, 3), three.Substring(i, 3)));
            }

            var stringVersion = new StringBuilder();

            foreach (var possibleOptions in numbers)
            {
                if (possibleOptions.Length == 0)
                {
                    stringVersion.Append('?');
                    continue;
                }
                // todo validate and look at other options
                stringVersion.Append(possibleOptions[0]);
            }
            return stringVersion.ToString();
        }

        private int[] ThreeChars(string first, string second, string thrid)
        {
            var block = first + second + thrid;
            return _guesser.Guesser(block);
        }
    }
}

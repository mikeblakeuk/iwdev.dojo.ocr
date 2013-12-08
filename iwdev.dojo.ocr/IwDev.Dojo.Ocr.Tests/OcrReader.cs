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
            return LinesToAccountNumbers(lines).Select(x => x.AccountNumber).ToArray();
        }

        public OcrResult[] LinesToAccountNumbers(string[] lines)
        {
            var numbers = new List<OcrResult>();
            // Check Line count is correct mod 4
            for (var i = 0; i < lines.Length - 3; i += 4)
            {
                // Check lines[i +3] == string.Empty;
                numbers.Add(ThreeLines(lines[i], lines[i + 1], lines[i + 2]));
            }
            return numbers.ToArray();
        }

        private OcrResult ThreeLines(string one, string two, string three)
        {
            var result = new OcrResult();
            var numbers = new List<int[]>();
            // Trim if too long?
            // Check blocks of 3
            // Check all the same length
            for (var i = 0; i < one.Length - 2; i += 3)
            {
                numbers.Add(ThreeChars(one.Substring(i, 3), two.Substring(i, 3), three.Substring(i, 3)));
            }

            result.AccountNumber = LinesToPossibleNumbers(numbers);

            if (result.AccountNumber.Contains('?'))
                return result;

            result.AccountNumberIsValid = _validator.IsValid(result.AccountNumber);
            result.AccountNumberOptions = GetPossibleAccountNumbers(numbers);

            return result;
        }

        private List<string> GetPossibleAccountNumbers(IEnumerable<int[]> numbers)
        {
            var accountCharIndex = 0;
            var accountNumbers = new List<string>();

            var possibleOptions = numbers.ToArray();
            var currentGuess = new int[possibleOptions.Count()];

            for (int i = 0; i < possibleOptions.Count(); i++)
            {
                currentGuess[i] = possibleOptions.ElementAt(i)[0];
            }

            foreach (var possibleOption in possibleOptions)
            {
                for (int i = 1; i < possibleOption.Length; i++)
                {
                    currentGuess[accountCharIndex] = possibleOption[i];
                    if (_validator.IsValid(currentGuess))
                        accountNumbers.Add(IntsToStrings(currentGuess));
                }
                currentGuess[accountCharIndex] = possibleOption[0];
                
                accountCharIndex++;
            }
            return accountNumbers;
        }

        private static string LinesToPossibleNumbers(List<int[]> numbers)
        {
            var stringVersion = new StringBuilder();

            foreach (var possibleOptions in numbers)
            {
                if (possibleOptions.Length == 0)
                {
                    stringVersion.Append('?');
                    continue;
                }
                stringVersion.Append(possibleOptions[0]);
            }
            var v = stringVersion.ToString();
            return v;
        }


        private static string IntsToStrings(int[] numbers)
        {
            var stringVersion = new StringBuilder();

            foreach (var possibleOptions in numbers)
            {
                stringVersion.Append(possibleOptions);
            }
            var v = stringVersion.ToString();
            return v;
        }

        private int[] ThreeChars(string first, string second, string thrid)
        {
            var block = first + second + thrid;
            return _guesser.Guesser(block);
        }
    }

    public class OcrResult
    {
        public string AccountNumber { get; set; }

        public List<string> AccountNumberOptions { get; set; }

        public bool AccountNumberIsValid { get; set; }
    }
}

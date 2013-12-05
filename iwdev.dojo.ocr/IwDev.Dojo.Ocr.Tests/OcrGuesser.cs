using System.Collections.Generic;
using System.Linq;

namespace IwDev.Dojo.Ocr.Tests
{
    public class OcrGuesser
    {
        public int[] Guesser(string data)
        {
            // Options here to add a cache to make it faster. data => int[] is fixed.
            var guesses = new List<int>();

            if (data.Length != 9)
                return guesses.ToArray();

            var cleanData = data;
            // Remove this dependancy (Ioc maybe)
            var known = OcrReader.Blocks.FirstOrDefault(x => x.Value == cleanData);
            if (known.Value != null)
                return new[] { known.Key };

            var underScores = new[] { 1, 4, 7 };

            for (var i = 0; i < cleanData.Length; i++)
            {
                var guess = cleanData;
                guess = underScores.Contains(i) 
                            ? guess.ReplaceAtIndex(i, cleanData[i] == ' ' ? '_' : ' ') 
                            : guess.ReplaceAtIndex(i, cleanData[i] == ' ' ? '|' : ' ');

                var known2 = OcrReader.Blocks.FirstOrDefault(x => x.Value == guess);
                if (known2.Value != null)
                    guesses.Add(known2.Key);
            }

            return guesses.ToArray();
        }
    }
}
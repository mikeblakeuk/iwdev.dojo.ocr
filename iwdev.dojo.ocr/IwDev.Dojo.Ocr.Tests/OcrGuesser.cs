using System.Collections.Generic;
using System.Linq;

namespace IwDev.Dojo.Ocr.Tests
{
    public class OcrGuesser
    {
        // Pass this in?
        public static readonly Dictionary<int, string> Blocks = new Dictionary<int, string>(10);

        static OcrGuesser()
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

        public int[] Guesser(string data)
        {
            // Options here to add a cache to make it faster. data => int[] is fixed.
            var guesses = new List<int>();

            if (data.Length != 9)
                return guesses.ToArray();

            // Could add extra guessing. eg Replace all unknown chars with ' ' or '.' with '_'
            var cleanData = data;
            var known = Blocks.FirstOrDefault(x => x.Value == cleanData);
            if (known.Value != null)
                return new[] { known.Key };

            var underScores = new[] { 1, 4, 7 };

            for (var i = 0; i < cleanData.Length; i++)
            {
                var guess = cleanData;
                guess = underScores.Contains(i) 
                            ? guess.ReplaceAtIndex(i, cleanData[i] == ' ' ? '_' : ' ') 
                            : guess.ReplaceAtIndex(i, cleanData[i] == ' ' ? '|' : ' ');

                var known2 = Blocks.FirstOrDefault(x => x.Value == guess);
                if (known2.Value != null)
                    guesses.Add(known2.Key);
            }

            return guesses.ToArray();
        }
    }
}
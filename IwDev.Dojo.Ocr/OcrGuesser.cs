using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IwDev.Dojo.Ocr
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

        public string UnGuesser(IEnumerable<int> ints)
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var sb3 = new StringBuilder();
            foreach (var i in ints)
            {
                if (Blocks.ContainsKey(i))
                {
                    sb1.Append(Blocks[i][0]);
                    sb1.Append(Blocks[i][1]);
                    sb1.Append(Blocks[i][2]);

                    sb2.Append(Blocks[i][3]);
                    sb2.Append(Blocks[i][4]);
                    sb2.Append(Blocks[i][5]);

                    sb3.Append(Blocks[i][6]);
                    sb3.Append(Blocks[i][7]);
                    sb3.Append(Blocks[i][8]);
                }
            }
            return sb1 + Environment.NewLine + sb2 + Environment.NewLine + sb3 + Environment.NewLine;
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
                guesses.Add(known.Key);

            var underScores = new[] { 1, 4, 7 };

            for (var i = 0; i < cleanData.Length; i++)
            {
                var charToReplace = underScores.Contains(i) ? '_' : '|'; 
                var guess = cleanData.ReplaceAtIndex(i, cleanData[i] == ' ' ? charToReplace : ' ');

                var known2 = Blocks.FirstOrDefault(x => x.Value == guess);
                if (known2.Value != null)
                    guesses.Add(known2.Key);
            }

            return guesses.ToArray();
        }
    }
}
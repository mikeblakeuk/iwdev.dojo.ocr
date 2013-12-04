using System.Collections.Generic;

namespace IwDev.Dojo.Ocr.Tests
{
    public class BlockToCharsEngine
    {
        private int numberOfRows = 3;
        private int charsPerBlock = 3;


        public string ConvertBlockToChars(string linesIn)
        {
            return "0";
        }

        public IList<string> ConvertLongLineToBlocks(string testData)
        {

            var theListOfBlocks = new List<string>();

            var currentBucket = 0;

            var iCharCounter = 0;

            // Take each char
            for (int i = 0; i < testData.Length; i++)
            {
                // Make sure bucket existis
                if (theListOfBlocks.Count < currentBucket + 1)
                    theListOfBlocks.Add(string.Empty);

                iCharCounter++;


                var aSingleChar = testData[i];
                theListOfBlocks[currentBucket] = theListOfBlocks[currentBucket] + aSingleChar;

                if (iCharCounter < 3)
                    continue;

                iCharCounter = 0;
                // 3rd char hit, move to next bucket
                currentBucket++;
            }

            var theRealBuckets = new List<string>();


            var thingsIHave = theListOfBlocks.Count/3;


            for (int i = 0; i < thingsIHave; i++)
            {
                var lineOneOffset = i;
                var lineTwoOffset = i + thingsIHave;
                var lineThreeOffset = i + (thingsIHave * 2);

                var all = theListOfBlocks[lineOneOffset]
                 + theListOfBlocks[lineTwoOffset]
                 + theListOfBlocks[lineThreeOffset];

                theRealBuckets.Add(all);
            }

            return theRealBuckets;
        }
    }
}
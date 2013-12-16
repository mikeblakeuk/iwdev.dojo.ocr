using System;
using System.Text;
using System.Threading.Tasks;

namespace IwDev.Dojo.Ocr
{
    public static class StringExtension
    {
        // Very slow. Should be using RegEx or StringBuilder
        public static string ReplaceAtIndex(this string word, int i, char value)
        {
            char[] letters = word.ToCharArray();
            letters[i] = value;
            return string.Join("", letters);
        }
    }
}

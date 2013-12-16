using System.Collections.Generic;
using System.Text;

namespace IwDev.Dojo.Ocr
{
    public static class ListExtensions
    {
        public static string Join(this IList<string> list, string joiner, string format)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < list.Count; i++)
            {
                if (i != 0)
                    sb.Append(joiner);
                sb.AppendFormat(format, list[i]);
            }
            return sb.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwDev.Dojo.Ocr.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(args[0]);
            var reader = new OcrReader(new OcrGuesser(), new AccountValidator());

            var results = reader.LinesToAccountNumbers(lines);

            File.WriteAllLines(args[0] + ".txt", results.Select(x => x.Display));
        }
    }
}

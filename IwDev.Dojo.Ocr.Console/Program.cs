using System.Diagnostics;
using System.IO;
using System.Linq;

namespace IwDev.Dojo.Ocr.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();

            var lines = File.ReadAllLines(args[0]);
            var reader = new OcrReader(new OcrGuesser(), new AccountValidator());

            var results = reader.LinesToAccountNumbers(lines);

            File.WriteAllLines(args[0] + ".txt", results.Select(x => x.Display));
            sw.Stop();
            System.Console.WriteLine(results.Count() + " in " + sw.ElapsedMilliseconds + "ms");
        }
    }
}

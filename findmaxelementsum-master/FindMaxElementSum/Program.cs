using System;
using System.Text;
using System.IO;
using FindMaxElementSum.Lib;
using FindMaxElementSum.Resources;

namespace FindMaxElementSum
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var path = string.Empty;
            if (args.Length > 0)
            {
                path = args[0];
            }

            while (!FileValidate.Validate(path))
            {
                Console.WriteLine(Messages.PromtPath);
                path = Console.ReadLine();
            }

            var analyzer = new DataAnalyzer(path);
            analyzer.Analyze();
            analyzer.Report();
        }
    }
}

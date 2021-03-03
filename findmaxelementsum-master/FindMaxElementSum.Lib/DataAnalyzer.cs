using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Linq;
using FindMaxElementSum.Lib.Resources;
using Console = System.Console;

namespace FindMaxElementSum.Lib
{
    public class DataAnalyzer
    {
        public string Path { get;}
        public decimal MaxSum { get; private set; } = 0;
        public List<int> MaxSumIndices { get;} = new List<int>();
        public List<int> BadStringsIndices { get;} = new List<int>();
        public bool IsAnalyzed { get; private set; } = false;

        public DataAnalyzer(string path)
        {
            Path = path;
        }

        public void Analyze()
        {
            if (IsAnalyzed)
            {
                return;
            }
            var counter = 0;
            string line;
            var maxSum = decimal.MinValue;
            using var file = new StreamReader(Path);
            while ((line = file.ReadLine()) != null)
            {
                var words = SplitString(line);
                counter++;
                if (!IsNumericLine(words))
                {
                    BadStringsIndices.Add(counter);
                    continue;
                }
                var numbers = ConvertToDecimals(words);
                var sum = CalculateSum(numbers);
                if (sum > maxSum)
                {
                    maxSum = sum;
                    MaxSumIndices.Clear();
                    MaxSumIndices.Add(counter);
                }
                else if (sum == maxSum)
                {
                    MaxSumIndices.Add(counter);
                }
            }
            MaxSum = maxSum;
            IsAnalyzed = true;
        }

        public static  List<string> SplitString(string source)
        {
            var words = source.Split(",").ToList();

            return words;
        }

        public static bool IsNumericLine(List<string> line)
        {
            foreach (var element in line)
            {
                if (decimal.TryParse(element, out _) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static List<decimal> ConvertToDecimals(List<string> source)
         {
             var output = new List<decimal>();
             foreach (var word in source)
             {
                 output.Add(decimal.Parse(word, CultureInfo.InvariantCulture));
             }

             return output;
         }

        public static decimal CalculateSum(List<decimal> sourceList)
         {
             decimal sum = 0;
             foreach (var number in sourceList)
             {
                 sum += number;
             }

             return sum;
         }

        public void Report()
        {
            //Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var sum = MaxSum.ToString(CultureInfo.InvariantCulture);
            if (MaxSumIndices.Count == 0)
            {
                sum = Messages.None;
            }
            Console.Write(Messages.Sum);
            Console.WriteLine(sum);
            Console.Write(Messages.AtFollowingIndices);
            Display(MaxSumIndices);
            Console.Write(Messages.FaultyLines);
            Display(BadStringsIndices);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void Display(List<int> source)
        {
            if (source.Count == 0)
            {
                Console.Write(Messages.None);
                return;
            }

            for (var i = 0; i < source.Count-1; i++)
            {
                Console.Write($"{source[i]}, ");
            }
            Console.WriteLine(source[^1]);
        }
    }
}
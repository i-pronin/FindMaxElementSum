using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace FindMaxElementSum.Lib
{
    public static class FileValidate
    {
        private static readonly List<string> AllowedExtensions = new List<string> { ".txt", ".csv" };
        public static bool Validate(string path)
        {
            return File.Exists(path) && IsCorrectFormat(path);
        }

        //case insensitive(Windows machines)
        public static bool IsCorrectFormat(string path)
        {
            if (path.Length <= 4) return false;
            var extension = path.Substring(path.Length - 4);
            Console.WriteLine($"{extension.ToLower()}");
            Console.WriteLine($"{extension.ToLower()} is equal to .txt or .csv: {AllowedExtensions.Contains(extension.ToLower())}");
            return AllowedExtensions.Contains(extension.ToLower());
        }
    }
}
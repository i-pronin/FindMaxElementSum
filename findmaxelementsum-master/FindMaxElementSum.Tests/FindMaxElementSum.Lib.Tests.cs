using FindMaxElementSum.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace FindMaxElementSum.Tests
{
    [TestClass]
    public class FindMaxElementSumLibTests
    {
        [DataTestMethod]
        [DataRow(@"..\netcoreapp3.1\Resources\EmptyFile.txt", true)]
        [DataRow(@"..\netcoreapp3.1\Resources\EmptyFile.csv", true)]
        [DataRow(@"..\netcoreapp3.1\Resources\EmptyFile.pxt", false)]
        [DataRow(@"..\netcoreapp3.1\Resources\EmptyFile5.TXT", true)]
        [DataRow(@"", false)]
        public void IsCorrectFormat_ShouldReturnTrueIfTxtOrCsv(string path, bool expected)
        {
            var actual = FileValidate.IsCorrectFormat(path);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(@"..\netcoreapp3.1\Resources\EmptyFile.txt", true)]
        [DataRow(@"..\netcoreapp3.1\Resources\EmptyFile.csv", false)]
        [DataRow(@"..\netcoreapp3.1\Resources\EmptyFile5.txt", false)]
        [DataRow(@"", false)]
        public void ValidateTest_ShouldReturnTrueIfTxtOrCsv(string path, bool expected)
        {
            var actual = FileValidate.Validate(path);
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(new string[] {"1", "2", "3", "4", "5"}, "1,2,3,4,5")]
        [DataRow(new string[] { "" }, "")]
        [DataRow(new string[] { "", "", "", ""}, ",,,")]

        public void SplitStringTest_ShouldReturnListOfString(string[] expectedArr, string source)
        {
            var actual = DataAnalyzer.SplitString(source);
            var expected = expectedArr.ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(new string[] { "1", "2", "3" }, true)]
        [DataRow(new string[] { "1", "2", "a" }, false)]
        [DataRow(new string[] { "" }, false)]
        public void IsNumericLineTest_ShouldReturnTrueIfStringCanBeConvertedIntoDecimal(string[] source, bool expected)
        {
            var actual = DataAnalyzer.IsNumericLine(source.ToList());

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(new string[] { "1", "2", "3" }, new double[] { 1.0, 2.0, 3.0 })]
        [DataRow(new string[] { "1.0", "2.5", "314" }, new double[] { 1.0, 2.5, 314.0 })]
        [DataRow(new string[] { "0" }, new double[] { 0.0 })]
        public void ConvertToDecimalTest_ShouldReturnListOfDecimalValues(string[] sourceArr, double[] expectedArr)
        {
            var source = sourceArr.ToList();
            var expected = expectedArr.Select(d => (decimal)d).ToList();


            var actual = DataAnalyzer.ConvertToDecimals(source);
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

            Trace.WriteLine("Expected: ");
            foreach (var element in expected)
            {
                Trace.Write($"{element} ");
            }

            Trace.WriteLine("Actual: ");
            foreach (var element in actual)
            {
                Trace.Write($"{element} ");
            }

            Trace.WriteLine(expected.ToString());
            Trace.WriteLine(actual.ToString());
            CollectionAssert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(new double[]{ 1.0, 2.0, 3.0, 4.5 }, 10.5)]
        [DataRow(new double[] {0}, 0)]
        [DataRow(new double[] { 0.1, 1.2, 2.3 }, 3.6 )]
        public void CalculateSumTest_ShouldReturnCorrectSumOfElements(double[] sourceArr, double expectedDouble)
        {
            decimal expected = (decimal) expectedDouble;
            var sourceList = sourceArr.Select(d=> (decimal) d).ToList();
        
            var actual = DataAnalyzer.CalculateSum(sourceList);
        
            Assert.AreEqual(actual, expected);
        }

        [DataTestMethod]
        [DataRow(15, new int[] {1}, new int[] {}, @"..\netcoreapp3.1\Resources\TestFile1.txt")]
        [DataRow(0, new int[] {2,3}, new int[] {1},@"..\netcoreapp3.1\Resources\TestFile2.txt")]
        [DataRow(8, new int[] {3},new int[] {1,2,4},@"..\netcoreapp3.1\Resources\TestFile3.txt")]
        [DataRow(6,new int[] {1,4},new int[] {2,5},@"..\netcoreapp3.1\Resources\TestFile4.txt")]
        public void AnalyzeTest_ShouldReturnCorrectSumAndIndices(double expectedDouble, int[] maxIndices, int[] badIndices, string path)
        {
            var analyzer = new DataAnalyzer(path);
            var expectedMaxSum = (decimal) expectedDouble;
            var expectedMaxIndices = maxIndices.ToList();
            var expectedBadIndices = badIndices.ToList();
            analyzer.Analyze();
            var maxSumValueEqual = analyzer.MaxSum == expectedMaxSum;
            var actualMaxIndices = analyzer.MaxSumIndices;
            var actualBadIndices = analyzer.BadStringsIndices;
            var indicesEqual = expectedMaxIndices.SequenceEqual(actualMaxIndices);
            var actual = maxSumValueEqual && indicesEqual;
            
            Assert.IsTrue(actual);
        }
    }
}

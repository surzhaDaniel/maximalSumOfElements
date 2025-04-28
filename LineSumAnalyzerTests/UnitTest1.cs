using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MaximalSumOfElements;

namespace MaximalSumOfElements.Tests
{
    [TestClass]
    public class LineSumAnalyzerTests
    {
        private string _tempFilePath;

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_tempFilePath))
            {
                File.Delete(_tempFilePath);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Constructor_ShouldThrowFileNotFound()
        {
            var args = new string[] { "nonExistingFile.txt" };

            var analyzer = new LineSumAnalyzer(args);
        }

        [TestMethod]
        public void AnalyzeFile_ShouldCalculateCorrectMaxSum()
        {
            var content = "1,2,3\n4,5,6\n7,8,9";
            _tempFilePath = Path.GetTempFileName();
            File.WriteAllText(_tempFilePath, content);

            var args = new string[] { _tempFilePath };
            var analyzer = new TestableLineSumAnalyzer(args);

            var (sums, brokenLines) = analyzer.TestAnalyzeFile();

            Assert.AreEqual(3, sums.Count);
            Assert.AreEqual(0, brokenLines.Count);
            var maxSumLine = sums.OrderByDescending(s => s.lineSum).First();
            Assert.AreEqual(3, maxSumLine.lineNumber);
            Assert.AreEqual(24, maxSumLine.lineSum);
        }

        [TestMethod]
        public void AnalyzeFile_ShouldCalculateCorrectMinSum()
        {
            var content = "1,2,3\n4,5,6\n7,8,9";
            _tempFilePath = Path.GetTempFileName();
            File.WriteAllText(_tempFilePath, content);

            var args = new string[] { _tempFilePath , "-min"};
            var analyzer = new TestableLineSumAnalyzer(args);

            var (sums, brokenLines) = analyzer.TestAnalyzeFile();

            Assert.AreEqual(3, sums.Count);
            Assert.AreEqual(0, brokenLines.Count);
            var minSumLine = sums.OrderBy(s => s.lineSum).First();
            Assert.AreEqual(1, minSumLine.lineNumber);
            Assert.AreEqual(6, minSumLine.lineSum);
        }

        [TestMethod]
        public void AnalyzeFile_ShouldDetectBrokenLines()
        {
            var content = "1,2,3\ninvalid,data\n7,8,9\n10, broken, 11";
            _tempFilePath = Path.GetTempFileName();
            File.WriteAllText(_tempFilePath, content);

            var args = new string[] { _tempFilePath };
            var analyzer = new TestableLineSumAnalyzer(args);

            var (sums, brokenLines) = analyzer.TestAnalyzeFile();

            Assert.AreEqual(2, sums.Count);
            Assert.AreEqual(2, brokenLines.Count);
            Assert.AreEqual(2, brokenLines[0]);
            Assert.AreEqual(4, brokenLines[1]);
        }
    }

    internal class TestableLineSumAnalyzer : LineSumAnalyzer
    {
        public TestableLineSumAnalyzer(string[] args) : base(args) { }

        public (List<(int lineNumber, double lineSum)>, List<int>) TestAnalyzeFile()
        {
            return base.AnalyzeFile();
        }
    }
}
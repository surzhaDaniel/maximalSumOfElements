using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MaximalSumOfElements;

namespace MaximalSumOfElements.Tests;

[TestClass]
public class FileSumsAnalyzerTests
{
    private string _tempFilePath;

    [TestCleanup]
    public void Cleanup()
    {
        if (!string.IsNullOrEmpty(_tempFilePath) && File.Exists(_tempFilePath))
        {
            File.Delete(_tempFilePath);
        }
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void Constructor_ShouldThrow_WhenFileDoesNotExist()
    {
        var analyzer = new FileSumsAnalyzer("nonexistent.txt", CalculationStrategy.Maximum);
    }

    [TestMethod]
    public void AnalyzeFile_ShouldReturnMaxSum_WithSingleLine()
    {
        _tempFilePath = CreateTempFile("1,2,3\n4,5,6\n7,8,9");

        var analyzer = new FileSumsAnalyzer(_tempFilePath, CalculationStrategy.Maximum);
        analyzer.AnalyzeFile();

        var sums = analyzer.GetLinesWithExtremeSum();
        var brokenLines = analyzer.GetBrokenLines();

        Assert.AreEqual(1, sums.Count);
        Assert.AreEqual(0, brokenLines.Count);
        Assert.AreEqual(3, sums[0].lineNumber);
        Assert.AreEqual(24, sums[0].lineSum);
    }

    [TestMethod]
    public void AnalyzeFile_ShouldReturnMinSum_WithSingleLine()
    {
        _tempFilePath = CreateTempFile("1,2,3\n4,5,6\n7,8,9");

        var analyzer = new FileSumsAnalyzer(_tempFilePath, CalculationStrategy.Minimum);
        analyzer.AnalyzeFile();

        var sums = analyzer.GetLinesWithExtremeSum();
        var brokenLines = analyzer.GetBrokenLines();

        Assert.AreEqual(1, sums.Count);
        Assert.AreEqual(0, brokenLines.Count);
        Assert.AreEqual(1, sums[0].lineNumber);
        Assert.AreEqual(6, sums[0].lineSum);
    }

    [TestMethod]
    public void AnalyzeFile_ShouldReturnMultipleLines_WithSameMaxSum()
    {
        _tempFilePath = CreateTempFile("1, 1, 1\n2,2,2\n3\n1,1,4");

        var analyzer = new FileSumsAnalyzer(_tempFilePath, CalculationStrategy.Minimum);
        analyzer.AnalyzeFile();

        var sums = analyzer.GetLinesWithExtremeSum();

        Assert.AreEqual(2, sums.Count);
        CollectionAssert.AreEquivalent(new List<int> { 1, 3 }, sums.Select(s => s.lineNumber).ToList());
        Assert.AreEqual(3, sums[0].lineSum);
    }

    [TestMethod]
    public void AnalyzeFile_ShouldDetectBrokenLines()
    {
        _tempFilePath = CreateTempFile("1,2,3\nbad,data\n3,3,3\nok,9,10");

        var analyzer = new FileSumsAnalyzer(_tempFilePath, CalculationStrategy.Maximum);
        analyzer.AnalyzeFile();

        var brokenLines = analyzer.GetBrokenLines();

        Assert.AreEqual(2, brokenLines.Count);
        CollectionAssert.AreEquivalent(new List<int> { 2, 4 }, brokenLines);
    }

    private string CreateTempFile(string content)
    {
        string path = Path.GetTempFileName();
        File.WriteAllText(path, content);
        return path;
    }
}

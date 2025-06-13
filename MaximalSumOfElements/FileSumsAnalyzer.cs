using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MaximalSumOfElements.Enums;
using MaximalSumOfElements.Factories;
using MaximalSumOfElements.Interfaces;

namespace MaximalSumOfElements;

/// <summary>
/// Analyzes a file containing sets of numbers and calculates line sums.
/// </summary>
public class FileSumsAnalyzer
{
    private readonly string _filePath;
    private readonly List<(int lineNumber, double lineSum)> _validLines = new();
    private readonly List<int> _brokenLines = new();
    private readonly ICalculationStrategy _strategy;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileSumsAnalyzer"/> class.
    /// </summary>
    /// <param name="filePath">Path to the input file.</param>
    /// <param name="strategyType">Type of calculation strategy to use.</param>
    /// <exception cref="FileNotFoundException">Thrown when the file does not exist.</exception>
    public FileSumsAnalyzer(string filePath, CalculationStrategyType strategyType)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File '{filePath}' not found.");
        }
        
        _filePath = filePath;
        _strategy = CalculationStrategyFactory.CreateStrategy(strategyType);
    }

    /// <summary>
    /// Analyzes the file and calculates sums for each line, marking broken lines.
    /// </summary>
    public void AnalyzeFile()
    {
        var numberFormat = CultureInfo.InvariantCulture;
        string[] lines = File.ReadAllLines(_filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',').Select(p => p.Trim()).ToArray();
            bool isBroken = false;
            double sum = 0;

            foreach (string part in parts)
            {
                if (double.TryParse(part, NumberStyles.Any, numberFormat, out double number))
                {
                    sum += number;
                }
                else
                {
                    isBroken = true;
                    break;
                }
            }

            if (isBroken)
            {
                _brokenLines.Add(i + 1);
            }
            else
            {
                _validLines.Add((i + 1, sum));
            }
        }
    }

    /// <summary>
    /// Gets the list of line numbers that contain non-numeric elements.
    /// </summary>
    /// <returns>A list of broken line numbers.</returns>
    public List<int> GetBrokenLines()
    {
        return new List<int> (_brokenLines);
    }

    /// <summary>
    /// Gets the lines with the extreme (maximum or minimum) sum based on the strategy.
    /// </summary>
    /// <returns>A list of tuples containing line numbers and their sums.</returns>
    public List<(int lineNumber, double lineSum)> GetLinesWithExtremeSum()
    {
        if (_validLines.Count == 0)
        {
            return new List<(int, double)>();
        }

        double target = _strategy.GetExtremeValue(_validLines.Select(x => x.lineSum));

        return _validLines
            .Where(x => x.lineSum == target)
            .ToList();
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MaximalSumOfElements;

public class FileSumsAnalyzer
{
    private readonly string _filePath;
    private readonly List<(int lineNumber, double lineSum)> _validLines = new();
    private readonly List<int> _brokenLines = new();
    public CalculationStrategy Strategy { get; }

    public FileSumsAnalyzer(string filePath, CalculationStrategy calculationStrategy)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File '{filePath}' not found.");
        }
        
        _filePath = filePath;
        Strategy = calculationStrategy;
    }

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

    public List<int> GetBrokenLines()
    {
        return new List<int> (_brokenLines);
    }

    public List<(int lineNumber, double lineSum)> GetLinesWithExtremeSum()
    {
        if (_validLines.Count == 0)
        {
            return new List<(int, double)>();
        }

        double target = Strategy == CalculationStrategy.Minimum
            ? _validLines.Min(x => x.lineSum)
            : _validLines.Max(x => x.lineSum);

        return _validLines
            .Where(x => x.lineSum == target)
            .ToList();
    }
}
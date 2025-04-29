using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MaximalSumOfElements;

public class FileSumsAnalyzer
{
    private readonly string _filePath;
    private readonly CalculationStrategy _strategy;

    public FileSumsAnalyzer(string filePath, CalculationStrategy calculationStrategy)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File '{filePath}' not found.");
        }
        
        _filePath = filePath;
        _strategy = calculationStrategy;
    }

    public (List<(int lineNumber, double lineSum)> sums, List<int> brokenLines) AnalyzeFile()
    {
        var numberFormat = CultureInfo.InvariantCulture;
        var sums = new List<(int lineNumber, double lineSum)>();
        var brokenLines = new List<int>();
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
                brokenLines.Add(i + 1);
            }
            else
            {
                sums.Add((i + 1, sum));
            }
        }
        if (!sums.Any())
        {
            return (new List<(int, double)>(), brokenLines);
        }

        double target = _strategy == CalculationStrategy.Minimum
            ? sums.Min(x => x.lineSum)
            : sums.Max(x => x.lineSum);
        var selectedSums = sums
            .Where(x => x.lineSum == target)
            .ToList();
        return (selectedSums,  brokenLines);
    }
}
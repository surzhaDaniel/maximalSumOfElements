using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MaximalSumOfElements
{
    public class LineSumAnalyzer
    {
        private const string MinOption = "-min";

        private readonly string _filePath;
        private readonly bool _findMin;

        public LineSumAnalyzer(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args.Length == 0)
            {
                _filePath = GetFileByInput();
            }
            else
            {
                _filePath = args[0];

                _findMin = args.Length > 1 && args[1] == MinOption;

                if (!File.Exists(_filePath))
                {
                    throw new FileNotFoundException("File not found.", _filePath);
                }
            }

            FindSum();
        }

        protected (List<(int lineNumber, double lineSum)>, List<int>) AnalyzeFile()
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

            return (sums, brokenLines);
        }

        private string GetFileByInput()
        {
            while (true)
            {
                Console.Write("Enter the path to the file: ");
                string inputPath = Console.ReadLine();

                if (File.Exists(inputPath))
                {
                    return inputPath;
                }
                else
                {
                    Console.WriteLine("File not found. Try again.");
                }
            }
        }

        private void FindSum()
        {
            try
            {
                var (sums, brokenLines) = AnalyzeFile();
                DisplayResults(sums, brokenLines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
        }

        private void DisplayResults(List<(int lineNumber, double lineSum)> sums, List<int> brokenLines)
        {
            Console.WriteLine("\nBroken lines:");
            if (brokenLines.Count == 0)
            {
                Console.WriteLine("None");
            }
            else
            {
                foreach (int line in brokenLines)
                {
                    Console.WriteLine(line);
                }
            }

            Console.WriteLine("\nSums:");

            if (sums.Count == 0)
            {
                Console.WriteLine("No valid lines found.");
            }
            else
            {
                foreach (var sum in sums)
                {
                    Console.WriteLine($"{sum.lineNumber}: {sum.lineSum}");
                }

                var result = _findMin
                    ? sums.OrderBy(x => x.lineSum).First()
                    : sums.OrderByDescending(x => x.lineSum).First();

                Console.WriteLine($"\nResult: Line {result.lineNumber} with sum {result.lineSum}");
            }
        }
    }
}

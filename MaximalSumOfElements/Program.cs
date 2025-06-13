using MaximalSumOfElements;
using MaximalSumOfElements.Enums;
using System;
using System.IO;
using System.Linq;

internal class Program
{
    private const string HelpOption = "-h";

    private static void Main(string[] args)
    {
        try
        {
            if (args.Length > 0 && args[0] == HelpOption)
            {
                DisplayHelp();
                return;
            }

            string filePath = args.Length > 0 ? args[0] : GetFileFromUser();
            CalculationStrategyType strategy = DetermineStrategy(args);
            FileSumsAnalyzer fileSumsAnalyzer = new(filePath, strategy);
            fileSumsAnalyzer.AnalyzeFile();
            PrintResult(fileSumsAnalyzer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Use -h for usage information.");
            Environment.Exit(1);
        }
    }
    private static CalculationStrategyType DetermineStrategy(string[] args)
    {
        if (args.Length < 2)
        {
            return CalculationStrategyType.Maximum;
        }
        return args[1] switch
        {
            "-min" => CalculationStrategyType.Minimum,
            "-max" => CalculationStrategyType.Maximum,
            _ => throw new ArgumentException($"Unknown mode '{args[1]}'. Use -min or -max.")
        };
    }

    private static string GetFileFromUser()
    {
        Console.Write("Enter file path: ");
        string path = Console.ReadLine();
        return path;
    }

    private static void PrintResult(FileSumsAnalyzer fileSumsAnalyzer)
    {
        var brokenLines = fileSumsAnalyzer.GetBrokenLines();
        var sums = fileSumsAnalyzer.GetLinesWithExtremeSum();

        Console.Write("\nBroken lines: ");
        Console.WriteLine(brokenLines.Count == 0
        ? "None"
        : string.Join(", ", brokenLines.Select(i => i.ToString())));

        if (sums.Count == 0)
        {
            Console.WriteLine("No valid lines found.");
        }
        else
        {
            Console.WriteLine("Extreme sum(s): ");

            foreach (var sum in sums)
            {
                Console.WriteLine($"Line '{sum.lineNumber}' with sum '{sum.lineSum}'");
            } 
        }
    }
    private static void DisplayHelp()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  yourcode.exe [filePath] [-min]");
        Console.WriteLine();
        Console.WriteLine("Arguments:");
        Console.WriteLine("  filePath          Path to the input file with number sets");
        Console.WriteLine("  '-min'            Find minimum sum instead of maximum sum");
        Console.WriteLine("  '-max' or empty   Find maximum sum instead of maximum sum");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  yourcode.exe c:\\yourFile.txt        // Find maximum sum");
        Console.WriteLine("  yourcode.exe c:\\yourFile.txt -max   // Find maximum sum");
        Console.WriteLine("  yourcode.exe c:\\yourFile.txt -min   // Find minimum sum");
        Console.WriteLine();
        Console.WriteLine("If no file path is provided, the program will prompt for input.");
    }
}

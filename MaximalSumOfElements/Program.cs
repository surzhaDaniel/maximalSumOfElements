using MaximalSumOfElements;
using System;
using System.IO;

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

            LineSumAnalyzer lineSumAnalyzer = new(args);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Use -h for usage information.");
            Environment.Exit(1);
        }
    }

    private static void DisplayHelp()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  yourcode.exe [filePath] [-min]");
        Console.WriteLine();
        Console.WriteLine("Arguments:");
        Console.WriteLine("  filePath    Path to the input file with number sets");
        Console.WriteLine("  -min        Find minimum sum instead of maximum sum");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  yourcode.exe c:\\yourFile.txt        // Find maximum sum");
        Console.WriteLine("  yourcode.exe c:\\yourFile.txt -min   // Find minimum sum");
        Console.WriteLine();
        Console.WriteLine("If no file path is provided, the program will prompt for input.");
    }
}

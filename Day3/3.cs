using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string lines = File.ReadAllText("../../../3.txt");
        int sum = CalculateSum(lines);
        Console.WriteLine($"Sum of numeric values: {sum}");
    }

    static int CalculateSum(string input)
    {
        string pattern = @"(\d*(?<=[^\d.\n\r].{140,142})\d+)|(\d+(?=.{140,142}[^\d.\n\r])\d*)|((?<=[^\d.\n\r])\d+)|(\d+(?=[^\d.\n\r]))";
        Regex regex = new Regex(pattern, RegexOptions.Singleline);

        int sum = regex.Matches(input)
                       .Cast<Match>()
                       .Select(match => int.Parse(match.Value))
                       .Sum();

        return sum;
    }
}
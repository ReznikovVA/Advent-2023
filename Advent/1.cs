using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static int ExtractCalibrationValue(string line)
    {
        var digits = line.Where(char.IsDigit).ToArray();
        if (digits.Length >= 1){
            return (digits[0] - '0') * 10 + (digits[digits.Length - 1] - '0');
        }
        return 0;
    }

    static int ExtractCalibrationValueSpelledOut(string line)
    {
        var sanitizedLine = DigitMapping.Aggregate(line, (current, value) =>
        current.Replace(value.Key, value.Value));

        var digits = sanitizedLine.Where(char.IsDigit).ToArray();
        if (digits.Length >= 1)
        {
            return (digits[0] - '0') * 10 + (digits[digits.Length - 1] - '0');
        }
        return 0;
    }

    static void Main()
    {
        string filePath = "../../../1.txt";
        string[] lines = File.ReadAllLines(filePath);
        int sum1 = lines.Select(ExtractCalibrationValue).Sum();
        Console.WriteLine($" 1.1 Sum of calibration values: {sum1}");

        int sum2 = lines.Select(ExtractCalibrationValueSpelledOut).Sum();
        Console.WriteLine($" 1.2 Sum of calibration values: {sum2}");
    }

    static readonly Dictionary<string, string> DigitMapping = new Dictionary<string, string>
    {
        {"one", "one1one"},
        {"two", "two2two"},
        {"three", "three3three"},
        {"four", "four4four"},
        {"five", "five5five"},
        {"six", "six6six"},
        {"seven", "seven7seven"},
        {"eight", "eight8eight"},
        {"nine", "nine9nine"}
    };
}
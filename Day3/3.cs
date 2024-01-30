using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        //1
        string lines = File.ReadAllText("../../../3.txt");
        int sum1 = CalculateSum(lines);
        Console.WriteLine($"Sum of numeric values: {sum1}");


        //2
        string[] lines2 = File.ReadAllLines("../../../3.txt");
        int sum2 = CalculateGearRatios(lines2);
        Console.WriteLine($"Sum of gear ratios: {sum2}");
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

    static int CalculateGearRatios(string[] lines)
    {
        int length = lines.Length;
        int width = lines[0].Length;
        int gear_ratio = 0;

        string gear_re = @"\*";
        string value_re = @"\d+";

        for (int idx = 0; idx < length; idx++)
        {
            string line = lines[idx];
            MatchCollection gearMatches = Regex.Matches(line, gear_re);
            foreach (Match gearMatch in gearMatches)
            {
                int minRange = Math.Max(gearMatch.Index - 1, 0);
                int maxRange = Math.Min(gearMatch.Index + gearMatch.Length, width);
                int num_of_adjacent = 0;
                int temp_gear_ratio = 1;
                for (int i = Math.Max(idx - 1, 0); i < Math.Min(idx + 2, length); i++)
                {
                    foreach (Match valueMatch in Regex.Matches(lines[i], value_re))
                    {
                        if (Enumerable.Range(minRange, maxRange - minRange + 1).Contains(valueMatch.Index) ||
                            Enumerable.Range(minRange, maxRange - minRange + 1).Contains(valueMatch.Index + valueMatch.Length - 1))
                        {
                            num_of_adjacent++;
                            temp_gear_ratio *= int.Parse(valueMatch.Value);
                        }
                    }
                }
                if (num_of_adjacent == 2)
                {
                    gear_ratio += temp_gear_ratio;
                }
            }
        }
        return gear_ratio;
    }
}
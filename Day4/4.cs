class Program
{
    static void Main()
    {
        int sum = 0;
        string[] lines = File.ReadAllLines("../../../day4.txt");
        foreach (string line in lines)
        {
            string[] parts = line.Split(": ");
            if (parts.Length != 2)
            {
                continue;
            }

            string line_trimmed = parts[1].Trim();

            string[] cards = line_trimmed.Split('|');

            string[] winNumbers = cards[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] trimmedWinNumbers = winNumbers.Select(s => s.Trim()).ToArray();

            string[] cardNumbers = cards[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] trimmedCardNumbers = cardNumbers.Select(s => s.Trim()).ToArray();

            var matchingCharacters = trimmedWinNumbers.Intersect(trimmedCardNumbers);
            Console.WriteLine(matchingCharacters.Count());
            Console.WriteLine(CalculatePoints(matchingCharacters.Count()));
            sum += CalculatePoints(matchingCharacters.Count());
        }

        Console.WriteLine(sum);
    }

    static int CalculatePoints(int count)
    {
         return count == 0 ? 0 : 1 << (count - 1);
    }
}
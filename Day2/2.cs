using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<string, int> desiredConfiguration = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        string filePath = "../../../2.txt";
        string[] games = File.ReadAllLines(filePath);

        Console.WriteLine($"Sum of IDs of possible games: {SumOfPossibleGameIds(games, desiredConfiguration)}");

        Console.WriteLine($"Sum of min powers: {SumOfMinQuantityOfCubesSets(games)}");
    }

    static int SumOfMinQuantityOfCubesSets(string[] games)
    {
        int sumOfPowers = 0;
        for (int gameId = 1; gameId <= games.Length; gameId++)
        {
            string gameWithoutPrefix = games[gameId - 1].Replace($"Game {gameId}: ", "");
            sumOfPowers += MinPower(gameWithoutPrefix);
        }
        return sumOfPowers;
    }

    static int MinPower(string game)
    {
        string[] subsets = game.Split(';');
        int maxRed = 0;
        int maxGreen = 0;
        int maxBlue = 0;

        foreach (var subset in subsets)
        {
            string[] cubes = subset.Split(',').Select(s => s.Trim()).ToArray();
            foreach (var cube in cubes)
            {
                string[] parts = cube.Split(' ');
                int count = int.Parse(parts[0]);
                string color = parts[1];
                switch (color)
                {
                    case ("red"):
                        if (count > maxRed) {
                            maxRed = count;
                        }
                        break;
                    case ("green"):
                        if (count > maxGreen){
                            maxGreen = count;
                        }
                        break;
                    case ("blue"):
                        if (count > maxBlue){
                            maxBlue = count;
                        }
                        break;
                }
            }
        }
        return (maxRed * maxGreen * maxBlue);
    }

    static int SumOfPossibleGameIds(string[] games, Dictionary<string, int> desiredConfiguration)
    {
        int sumOfPossibleGameIds = 0;
        for (int gameId = 1; gameId <= games.Length; gameId++)
        {
            string gameWithoutPrefix = games[gameId - 1].Replace($"Game {gameId}: ", "");
            if (IsGamePossible(gameWithoutPrefix, desiredConfiguration))
            {
                sumOfPossibleGameIds += gameId;
            }
        }
        return sumOfPossibleGameIds;
        
    }

    static bool IsGamePossible(string game, Dictionary<string, int> desiredConfiguration)
    {
        Dictionary<string, int> cubeCounts = new Dictionary<string, int>(desiredConfiguration);
        string[] subsets = game.Split(';');

        foreach (var subset in subsets)
        {
            string[] cubes = subset.Split(',').Select(s => s.Trim()).ToArray();

            foreach (var cube in cubes)
            {
                string[] parts = cube.Split(' ');
                int count = int.Parse(parts[0]);
                string color = parts[1];

                if (cubeCounts[color] < count)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
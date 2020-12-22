using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day15 : IPuzzle
    {
        public async Task Solve()
        {
            var inputData = (await InputDataReader.GetInputDataAsync<int>("Day15_1.txt", ",")).ToList();

            Part1(inputData);
            Part2(inputData);
        }

        private static void Part1(List<int> inputData)
        {
            var lastNumberSpoke = GetLastNumberSpoke(inputData, 2020);

            Console.WriteLine($"Part1: {lastNumberSpoke}");
        }

        private static void Part2(List<int> inputData)
        {
            var lastNumberSpoke = GetLastNumberSpoke(inputData, 30000000);

            Console.WriteLine($"Part2: {lastNumberSpoke}");
        }

        private static int GetLastNumberSpoke(List<int> inputData, int turns)
        {
            var numbersSpoke = inputData.ToDictionary(d => d, _ => 0);

            var turn = 0;
            var startingNumberCount = numbersSpoke.Count;
            var lastNumberSpoke = numbersSpoke.Last().Key;

            while (turn < turns)
            {
                var currentNumber = lastNumberSpoke;
                turn++;

                if (turn > startingNumberCount)
                {
                    if (numbersSpoke.TryGetValue(lastNumberSpoke, out int previousTurnSpoken) && previousTurnSpoken > 0)
                    {
                        lastNumberSpoke = turn - 1 - previousTurnSpoken;
                    }
                    else
                    {
                        lastNumberSpoke = 0;
                    }

                    numbersSpoke[currentNumber] = turn - 1;
                }
                else
                {
                    lastNumberSpoke = numbersSpoke.ElementAt(turn - 1).Key;
                    numbersSpoke[lastNumberSpoke] = turn < startingNumberCount ? turn : 0;
                }
            }

            return lastNumberSpoke;
        }
    }
}

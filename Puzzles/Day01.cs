using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    internal class Day01 : IPuzzle
    {
        public async Task Solve()
        {
            var inputData = await InputDataReader.GetInputDataAsync<int>("Day01.txt");

            Part1(inputData);
            Part2(inputData);
        }

        private static void Part1(IEnumerable<int> inputData)
        {
            var match = inputData.FirstOrDefault(i => inputData.Contains(2020 - i));
            Console.WriteLine($"Part1: {match * (2020 - match)}");
        }

        private static void Part2(IEnumerable<int> inputData)
        {
            foreach (var input in inputData)
            {
                var match = inputData.FirstOrDefault(i => inputData.Contains(2020 - input - i));
                var match2 = 2020 - input - match;

                if (match > 0 && match2 > 0)
                {
                    Console.WriteLine($"Part2: {input * match * match2}");
                    break;
                }
            }
        }
    }
}

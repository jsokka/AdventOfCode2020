using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day9 : IPuzzle
    {
        public async Task Solve()
        {
            var inputData = (await InputDataReader.GetInputDataAsync<long>("Day9_1.txt")).ToList();

            Part1(inputData);
            Part2(inputData);
        }

        private static void Part1(List<long> inputData)
        {
            var invalidNumber = GetInvalidNumber(inputData, 25);

            Console.WriteLine($"Part1: {invalidNumber}");
        }

        private static void Part2(List<long> inputData)
        {
            var invalidNumber = GetInvalidNumber(inputData, 25);

            var set = FindSet(inputData, invalidNumber);

            Console.WriteLine($"Part2: {set.Max() + set.Min()}");
        }

        private static long GetInvalidNumber(List<long> inputData, int preambleLength)
        {
            long val = 0;

            for (int i = preambleLength; i < inputData.Count; i++)
            {
                val = inputData[i];

                var preamble = inputData.Skip(i - preambleLength).Take(preambleLength);

                if (!preamble.Any(p => preamble.Any(p2 => p2 != p && p + p2 == val)))
                {
                    return val;
                }
            }

            throw new Exception("Should not end up here :<");
        }

        private static List<long> FindSet(List<long> inputData, long invalidNumber)
        {
            for (int i = 0; i < inputData.Count; i++)
            {
                var set = new List<long>() { inputData[i] };

                for (int j = i + 1; j < inputData.Count; j++)
                {
                    set.Add(inputData[j]);

                    if (set.Sum() == invalidNumber)
                    {
                        return set;
                    }
                }
            }

            throw new Exception("Should not end up here :<");
        }
    }
}

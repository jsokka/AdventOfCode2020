using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day10 : IPuzzle
    {
        private static readonly Dictionary<int, int> Multipliers = new Dictionary<int, int>()
        {
            { 1, 1 },
            { 2, 1 },
            { 3, 2 },
            { 4, 4 },
            { 5, 7 }
        };

        public async Task Solve()
        {
            var inputData = (await InputDataReader.GetInputDataAsync<int>("Day10.txt"))
                .OrderBy(i => i).ToList();

            Part1(inputData);
            Part2(inputData);
        }

        private static void Part1(List<int> inputData)
        {
            var adapters = new List<int>(inputData);

            int i = 0;
            var differences = new Dictionary<int, int>();

            adapters.Insert(0, 0);
            adapters.Add(inputData.Last() + 3);

            while (true)
            {
                var current = adapters[i];
                var next = adapters.FirstOrDefault(j => adapters.IndexOf(j) > i && j - current <= 3);

                if (next == 0)
                {
                    break;
                }

                differences.Add(next, next - current);

                i = adapters.IndexOf(next);
            }

            Console.WriteLine($"Part1: {differences.Count(d => d.Value == 1) * differences.Count(d => d.Value == 3)}");
        }

        private static void Part2(List<int> inputData)
        {
            var adapters = new List<int>(inputData);

            adapters.Insert(0, 0);
            adapters.Add(inputData.Last() + 3);

            var series = new List<int>();
            var numbersInSerie = 1;

            for (int i = 0; i < inputData.Count; i++)
            {
                var current = adapters[i];
                var next = adapters.FirstOrDefault(j => adapters.IndexOf(j) > i && j - current <= 3);

                if (next - current == 3)
                {
                    series.Add(numbersInSerie);
                    numbersInSerie = 0;
                }

                numbersInSerie++;
            }

            series.Add(numbersInSerie);

            long result = 1;

            foreach (var serie in series)
            {
                result *= Multipliers[serie];
            }

            Console.WriteLine($"Part2: {result}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day13 : IPuzzle
    {
        public async Task Solve()
        {
            var inputData = (await InputDataReader.GetInputDataAsync<string>("Day13.txt")).ToList();

            var earliestTime = int.Parse(inputData[0]);
            var busses = inputData[1].Split(',')
                .Where(x => x != "x").ToList().ConvertAll(int.Parse).ToHashSet();

            Part1(earliestTime, busses);
            Part2(inputData);
        }

        private static void Part1(int earliestTime, HashSet<int> busses)
        {
            var bussTimes = new Dictionary<int, HashSet<int>>();

            foreach (var bus in busses)
            {
                var times = new HashSet<int> { 0 };

                int time = 0;

                while (time <= earliestTime)
                {
                    time += bus;
                    times.Add(time);
                }

                bussTimes.Add(bus, times);
            }

            var differences = bussTimes.ToDictionary(bt => bt.Key, bt => bt.Value.Where(t => t >= earliestTime).Min(t => t - earliestTime));
            var min = differences.Single(d => d.Value == differences.Values.Min());

            Console.WriteLine($"Part1: {min.Value * min.Key}");
        }

        private static void Part2(List<string> inputData)
        {
            Console.WriteLine($"Part2: -");
        }
    }
}

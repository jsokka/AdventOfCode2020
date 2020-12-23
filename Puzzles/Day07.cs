using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day07 : IPuzzle
    {
        private Dictionary<string, Dictionary<string, int>> bags;

        public async Task Solve()
        {
            bags = await GetInputData();

            Part1();
            Part2();
        }

        private static async Task<Dictionary<string, Dictionary<string, int>>> GetInputData()
        {
            var inputData = await InputDataReader.GetInputDataAsync<string>("Day07.txt");

            var outerMostBagRegex = new Regex(@"^\w+ \w+");
            var innerBagsRegex = new Regex(@"([0-9]) (\w+ \w+) bag");

            var result = new Dictionary<string, Dictionary<string, int>>();

            foreach (var row in inputData)
            {
                var innerBags = new Dictionary<string, int>();

                var outerMostBag = outerMostBagRegex.Match(row).Value;
                var innerBagMatches = innerBagsRegex.Matches(row);

                if (innerBagMatches.Count > 0)
                {
                    innerBags = innerBagMatches.ToDictionary(
                        m => m.Groups[2].Value,
                        m => int.Parse(m.Groups[1].Value)
                    );
                }

                result.Add(outerMostBag, innerBags);
            }

            return result;
        }

        private void Part1()
        {
            var count = bags.Count(b => EventuallyCanContainBag(b.Key, "shiny gold"));

            Console.WriteLine($"Part1: {count}");
        }

        private void Part2()
        {
            var count = CountBags("shiny gold");

            Console.WriteLine($"Part2: {count}");
        }

        private bool EventuallyCanContainBag(string outerBag, string bagToFind)
        {
            // Directly
            if (bags[outerBag].Any(inner => inner.Key == bagToFind))
            {
                return true;
            }

            // Indirectly
            foreach (var innerBag in bags[outerBag])
            {
                if (EventuallyCanContainBag(innerBag.Key, bagToFind))
                {
                    return true;
                }
            }

            return false;
        }

        private int CountBags(string outerBag)
        {
            // Direct bags
            int sum = bags[outerBag].Sum(inner => inner.Value);

            // Indirect bags
            foreach (var innerBag in bags[outerBag])
            {
                sum += innerBag.Value * CountBags(innerBag.Key);
            }

            return sum;
        }
    }
}

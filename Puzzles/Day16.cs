using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day16 : IPuzzle
    {
        record Field(string Name, List<int[]> Ranges);

        public async Task Solve()
        {
            var inputData = (await InputDataReader.GetInputDataAsync<string>("Day16.txt")).ToList();

            var fields = ParseFields(inputData).ToList();
            var nearbyTickets = ParseNearbyTickets(inputData).ToList();
            var myTicket = inputData[inputData.IndexOf("your ticket:") + 1]
                .Split(",").Select(int.Parse).ToArray();

            Part1(fields, nearbyTickets);
            Part2(fields, nearbyTickets, myTicket);
        }

        private static void Part1(List<Field> fields, List<int[]> nearbyTickets)
        {
            var sum = 0;

            foreach (var ticket in nearbyTickets)
            {
                foreach (var val in ticket)
                {
                    if (!fields.Any(f => IsInRange(val, f)))
                    {
                        sum += val;
                    }
                }
            }

            Console.WriteLine($"Part1: {sum}");
        }

        private static void Part2(List<Field> fields, List<int[]> nearbyTickets, int[] myTicket)
        {
            var validTickets = new List<int[]>();

            foreach (var ticket in nearbyTickets)
            {
                var validTicket = true;

                foreach (var val in ticket)
                {
                    if (!fields.Any(f => IsInRange(val, f)))
                    {
                        validTicket = false;
                        break;
                    }
                }

                if (validTicket)
                {
                    validTickets.Add(ticket);
                }
            }

            var validFieldsByPosition = new Dictionary<int, string[]>();

            long result = 1;

            for (int i = 0; i < validTickets[0].Length; i++)
            {
                validFieldsByPosition[i] = fields.Where(f => validTickets
                    .All(t => IsInRange(t[i], f))).Select(f => f.Name).ToArray();
            }

            var orderedFields = new string[validFieldsByPosition.Count];

            foreach (var pf in validFieldsByPosition.OrderBy(f => f.Value.Length))
            {
                var field = pf.Value.SingleOrDefault(f => !orderedFields.Contains(f));
                orderedFields[pf.Key] = field;

                if (field.StartsWith("departure"))
                {
                    result *= myTicket[pf.Key];
                }
            }

            Console.WriteLine($"Part2: {result}");
        }

        private static bool IsInRange(int value, Field field)
        {
            return (field.Ranges[0][0] <= value && field.Ranges[0][1] >= value)
                || (field.Ranges[1][0] <= value && field.Ranges[1][1] >= value);
        }

        private static IEnumerable<Field> ParseFields(List<string> inputData)
        {
            foreach (var row in inputData)
            {
                if (row?.Length == 0)
                {
                    break;
                }

                var parts = row.Split(": ");
                var field = parts[0];
                var rangeParts = parts[1].Split(" or ");
                var range1 = rangeParts[0].Split("-").Select(int.Parse).ToArray();
                var range2 = rangeParts[1].Split("-").Select(int.Parse).ToArray();

                var ranges = new List<int[]> { range1, range2 };

                yield return new Field(field, new List<int[]> { range1, range2 });
            }
        }

        private static IEnumerable<int[]> ParseNearbyTickets(List<string> inputData)
        {
            var read = false;

            foreach (var row in inputData)
            {
                if (!read && row == "nearby tickets:")
                {
                    read = true;
                    continue;
                }

                if (read)
                {
                    yield return row.Split(",").Select(int.Parse).ToArray();
                }
            }
        }
    }
}

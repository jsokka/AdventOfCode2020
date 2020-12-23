using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day06 : IPuzzle
    {
        public async Task Solve()
        {
            var inputData = await GetInputData();

            Part1(inputData);
            Part2(inputData);
        }

        private async Task<IEnumerable<string>> GetInputData()
        {
            var inputData = await InputDataReader.GetInputDataAsync<string>("Day06.txt");
            var cleanedInputData = new List<string>();
            var newRow = string.Empty;

            foreach (var row in inputData)
            {
                if (string.IsNullOrWhiteSpace(row))
                {
                    cleanedInputData.Add(newRow.Trim());
                    newRow = "";
                    continue;
                }

                newRow += " " + row.Trim();
            }

            if (!string.IsNullOrWhiteSpace(newRow))
            {
                cleanedInputData.Add(newRow.Trim());
            }

            return cleanedInputData;
        }

        private static void Part1(IEnumerable<string> inputData)
        {
            var sum = inputData.Sum(s => s.Distinct().Count(c => c != ' '));

            Console.WriteLine($"Part1: {sum}");
        }

        private static void Part2(IEnumerable<string> inputData)
        {
            var sum = 0;

            foreach (var group in inputData)
            {
                var groupAnswers = group.Split(' ');
                var countedAnswers = new HashSet<char>();
                foreach (var answers in groupAnswers)
                {
                    sum += answers.Distinct().Count(a => !countedAnswers.Contains(a) &&
                        groupAnswers.All(ga => ga.Contains(a)));
                    foreach (var counted in answers)
                    {
                        countedAnswers.Add(counted);
                    }
                }
            }

            Console.WriteLine($"Part2: {sum}");
        }
    }
}

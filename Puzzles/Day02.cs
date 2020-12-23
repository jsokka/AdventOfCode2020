using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    internal class Day02 : IPuzzle
    {
        public async Task Solve()
        {
            var inputData = await InputDataReader.GetInputDataAsync<string>("Day02.txt");

            Part1(inputData);
            Part2(inputData);
        }

        private static void Part1(IEnumerable<string> inputData)
        {
            var validPasswords = 0;

            foreach (var row in inputData)
            {
                var password = GetPassword(row);
                var policy = GetPolicyLetter(row, out int min, out int max);

                var policyLetterCount = password.Count(c => c == policy);

                if (policyLetterCount >= min && policyLetterCount <= max)
                {
                    validPasswords++;
                }
            }

            Console.WriteLine($"Part1: {validPasswords}");
        }

        private static void Part2(IEnumerable<string> inputData)
        {
            var validPasswords = 0;

            foreach (var row in inputData)
            {
                var password = GetPassword(row);
                var policyLetter = GetPolicyLetter(row, out int pos1, out int pos2);

                if (password[pos1 - 1] == policyLetter ^ password[pos2 - 1] == policyLetter)
                {
                    validPasswords++;
                }
            }

            Console.WriteLine($"Part2: {validPasswords}");
        }

        private static char GetPolicyLetter(string row, out int min, out int max)
        {
            var policy = row[..row.IndexOf(':')].Trim();
            var parts = policy.Split(' ');
            var minMaxParts = parts[0].Split('-');

            min = int.Parse(minMaxParts[0]);
            max = int.Parse(minMaxParts[1]);

            return parts[1][0];
        }

        private static string GetPassword(string row)
        {
            return row[(row.IndexOf(':') + 1)..].Trim();
        }
    }
}

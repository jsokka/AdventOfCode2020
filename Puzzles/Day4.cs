using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day4 : IPuzzle
    {
        private readonly Dictionary<string, Func<string, bool>> requiredFields
            = new Dictionary<string, Func<string, bool>>
        {
            { "byr", (val) => int.TryParse(val, out int year) && year >= 1920 && year <= 2002 },
            { "iyr", (val) => int.TryParse(val, out int year) && year >= 2010 && year <= 2020 },
            { "eyr", (val) => int.TryParse(val, out int year) && year >= 2020 && year <= 2030 },
            { "hgt", (val) =>
                (val.EndsWith("cm") && int.TryParse(val.Replace("cm", ""), out int cm) && cm >= 150 && cm <= 193) ||
                (val.EndsWith("in") && int.TryParse(val.Replace("in", ""), out int inch) && inch >= 59 && inch <= 76) },
            { "hcl", (val) => new Regex("^#[0-9a-f]{6}$").IsMatch(val) },
            { "ecl", (val) => new[] { "amb", "blu", "brn", "gry", "grn", "hzl" ,"oth" }.Contains(val) },
            { "pid", (val) => new Regex("^[0-9]{9}$").IsMatch(val) }
        };

        public async Task Solve()
        {
            var inputData = await GetInputData();

            Part1(inputData);
            Part2(inputData);
        }

        private static async Task<IEnumerable<string>> GetInputData()
        {
            var inputData = await InputDataReader.GetInputDataAsync<string>("Day4_1.txt");
            var emptyLines = inputData.Count(l => string.IsNullOrWhiteSpace(l));
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

        private void Part1(IEnumerable<string> inputData)
        {
            var validPassports = 0;

            foreach (var passport in inputData)
            {
                if (HasRequiredFields(passport))
                {
                    validPassports++;
                }
            }

            Console.WriteLine($"Part1: {validPassports}");
        }

        private void Part2(IEnumerable<string> inputData)
        {
            var validPasswords = 0;

            foreach (var passport in inputData)
            {
                if (HasRequiredFields(passport) && HasValidFieldValues(passport))
                {
                    validPasswords++;
                }
            }

            Console.WriteLine($"Part2: {validPasswords}");
        }

        private bool HasRequiredFields(string passport)
        {
            return requiredFields.All(f => passport.Contains(f.Key));
        }

        private bool HasValidFieldValues(string passport)
        {
            var fieldValues = passport.Split(' ')
                .Select(f => f.Split(':'))
                .ToDictionary(f => f[0], f => f[1]);

            return fieldValues
                .Where(f => requiredFields.ContainsKey(f.Key))
                .All(f => requiredFields[f.Key](f.Value));
        }
    }
}

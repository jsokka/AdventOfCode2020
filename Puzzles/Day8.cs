using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day8 : IPuzzle
    {
        private record Operation(string Name, int Argument);

        public async Task Solve()
        {
            var operations = (await GetOperations()).ToList();

            Part1(operations);
            Part2(operations);
        }

        private static async Task<IEnumerable<Operation>> GetOperations()
        {
            var inputData = await InputDataReader.GetInputDataAsync<string>("Day8_1.txt");

            return inputData.Select(row =>
            {
                var parts = row.Split(' ');
                return new Operation(parts[0], int.Parse(parts[1]));
            });
        }

        private static void Part1(List<Operation> operations)
        {
            RunOperations(operations, out int acc);

            Console.WriteLine($"Part1: {acc}");
        }

        private static void Part2(List<Operation> operations)
        {
            int acc;
            int i = 0;

            while (true)
            {
                var op = operations[i];

                if (op.Name == "nop" || op.Name == "jmp")
                {
                    var alteredOperations = new List<Operation>(operations)
                    {
                        [i] = new Operation(op.Name == "nop" ? "jmp" : "nop", op.Argument)
                    };

                    if (RunOperations(alteredOperations, out acc))
                    {
                        break;
                    }
                }

                i++;
            }

            Console.WriteLine($"Part2: {acc}");
        }

        private static bool RunOperations(List<Operation> operations, out int acc)
        {
            var i = 0;
            var executedOperations = new List<int>();

            acc = 0;

            while (!executedOperations.Contains(i))
            {
                if (i == operations.Count)
                {
                    return true;
                }

                var op = operations[i];

                executedOperations.Add(i);

                switch (op.Name)
                {
                    case "nop":
                        i++;
                        break;
                    case "jmp":
                        i += op.Argument;
                        break;
                    case "acc":
                        acc += op.Argument;
                        i++;
                        break;
                }
            }

            return false;
        }
    }
}

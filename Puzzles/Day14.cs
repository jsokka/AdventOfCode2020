using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day14 : IPuzzle
    {
        public async Task Solve()
        {
            var inputData = (await InputDataReader.GetInputDataAsync<string>("Day14.txt")).ToList();

            Part1(inputData);
            Part2(inputData);
        }

        private static void Part1(List<string> inputData)
        {
            string mask = null;
            var memory = new Dictionary<int, long>();

            foreach (var row in inputData)
            {
                if (row.StartsWith("mask"))
                {
                    mask = row.Split(" = ")[1];
                }
                else
                {
                    var value = ParseValue(row, out int memoryAddress);
                    memory[memoryAddress] = ApplyMask(value, mask);
                }
            }

            Console.WriteLine($"Part1: {memory.Sum(m => m.Value)}");
        }

        private static void Part2(List<string> inputData)
        {
            string mask = null;
            var memory = new Dictionary<long, long>();

            foreach (var row in inputData)
            {
                if (row.StartsWith("mask"))
                {
                    mask = row.Split(" = ")[1];
                }
                else
                {
                    var value = ParseValue(row, out int memoryAddress);
                    Decode(memory, memoryAddress, value, mask);
                }
            }

            Console.WriteLine($"Part2: {memory.Sum(m => m.Value)}");
        }

        private static long ParseValue(string row, out int memoryAddress)
        {
            var parts = row.Split(" = ");
            var mem = int.Parse(parts[0].Replace("mem[", "").Replace("]", ""));
            var value = long.Parse(parts[1]);

            memoryAddress = mem;

            return value;
        }

        private static long ApplyMask(long value, string mask)
        {
            var binaryValue = Convert.ToString(value, 2).PadLeft(36, '0');
            var masked = "";

            for (int i = 0; i < binaryValue.Length; i++)
            {
                var maskBit = mask[i];
                masked += maskBit == 'X' ? binaryValue[i] : maskBit;
            }

            return Convert.ToInt64(masked, 2);
        }

        private static void Decode(Dictionary<long, long> memory, int memoryAddress, long value, string mask)
        {
            var binaryAddress = Convert.ToString(memoryAddress, 2).PadLeft(36, '0');
            var maskedAddress = "";

            for (int i = 0; i < binaryAddress.Length; i++)
            {
                var maskBit = mask[i];
                maskedAddress += maskBit == '0' ? binaryAddress[i] : maskBit;
            }

            var floatingCount = maskedAddress.Count(ma => ma == 'X');

            var combinations = Enumerable.Range(0, (int)Math.Pow(2, floatingCount))
                .Select(i => Enumerable.Range(0, floatingCount).Select(j => (i & (1 << j)) > 0 ? '1' : '0').ToList())
                .ToList();

            var addressToWrite = new List<int>();

            foreach (var combination in combinations)
            {
                var address = "";
                var j = 0;

                for (int i = 0; i < maskedAddress.Length; i++)
                {
                    var b = maskedAddress[i];

                    if (b == 'X')
                    {
                        b = combination[j];
                        j++;
                    }

                    address += b;
                }

                memory[Convert.ToInt64(address, 2)] = value;
            }
        }
    }
}

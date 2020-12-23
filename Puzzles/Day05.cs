using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day05 : IPuzzle
    {
        private record Seat(int Row, int Column, int Id);

        public async Task Solve()
        {
            var inputData = await InputDataReader.GetInputDataAsync<string>("Day05.txt");

            Part1(inputData);
            Part2(inputData);
        }

        private static void Part1(IEnumerable<string> inputData)
        {
            var seats = inputData.Select(ResolveSeat).ToList();

            Console.WriteLine($"Part1: {seats.Max(r => r.Id)}");
        }

        private static void Part2(IEnumerable<string> inputData)
        {
            var seats = inputData.Select(ResolveSeat).ToList();

            var missing = Enumerable.Range(seats.Min(s => s.Id), seats.Count)
                .Except(seats.Select(s => s.Id));

            Console.WriteLine($"Part2: {missing.First()}");
        }

        private static Seat ResolveSeat(string number)
        {
            int minRow = 0;
            int maxRow = 127;
            int minCol = 0;
            int maxCol = 7;

            foreach (var c in number)
            {
                int midRow = (maxRow + minRow) / 2;
                int midCol = (maxCol + minCol) / 2;

                switch (c)
                {
                    case 'F':
                        maxRow = midRow;
                        break;
                    case 'B':
                        minRow = midRow + 1;
                        break;
                    case 'L':
                        maxCol = midCol;
                        break;
                    case 'R':
                        minCol = midCol + 1;
                        break;
                }
            }

            return new Seat(minRow, minCol, (minRow * 8) + minCol);
        }
    }
}

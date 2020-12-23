using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day03 : IPuzzle
    {
        private record Move(int Right, int Down);

        private int columnCount;
        private int rowCount;

        public async Task Solve()
        {
            var inputData = (await InputDataReader.GetInputDataAsync<string>("Day03.txt")).ToList();

            columnCount = inputData[0].Length;
            rowCount = inputData.Count;

            Part1(inputData);
            Part2(inputData);
        }

        public void Part1(List<string> inputData)
        {
            int treeCount = TobogganAndCountTrees(inputData, 3, 1);
            Console.WriteLine($"Part1: {treeCount}");
        }

        public void Part2(List<string> inputData)
        {
            var moves = new[]
            {
                new Move(1, 1),
                new Move(3, 1),
                new Move(5, 1),
                new Move(7, 1),
                new Move(1, 2)
            };

            long result = 1;

            foreach (var move in moves)
            {
                result *= TobogganAndCountTrees(inputData, move.Right, move.Down);
            }

            Console.WriteLine($"Part2: {result}");
        }

        private int TobogganAndCountTrees(List<string> inputData, int stepsRight, int stepsDown)
        {
            int currentColumn = 0;
            int treeCount = 0;

            for (int row = 0; row < rowCount; row += stepsDown)
            {
                if (row == 0)
                {
                    continue;
                }

                currentColumn += stepsRight;

                if (currentColumn >= columnCount)
                {
                    currentColumn -= columnCount;
                }

                if (inputData[row][currentColumn] == '#')
                {
                    treeCount++;
                }
            }

            return treeCount;
        }
    }
}

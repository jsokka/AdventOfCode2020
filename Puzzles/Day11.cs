using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day11 : IPuzzle
    {
        public async Task Solve()
        {
            var inputData = (await InputDataReader.GetInputDataAsync<string>("Day11.txt"))
                .Select(c => c.ToCharArray()).ToList();

            Part1(inputData);
            Part2(inputData);
        }

        private static void Part1(List<char[]> inputData)
        {
            var layout = GetFinalLayout(inputData, 3);

            Console.WriteLine($"Part1: {layout.Sum(row => row.Count(col => col == '#'))}");
        }

        private static void Part2(List<char[]> inputData)
        {
            var layout = GetFinalLayout(inputData, 4, true);

            Console.WriteLine($"Part2: {layout.Sum(row => row.Count(col => col == '#'))}");
        }

        private static List<char[]> GetFinalLayout(List<char[]> initialLayout, int tolerance, bool checkAllVisibleSeats = false)
        {
            var currentLayout = new List<char[]>(initialLayout);

            //currentLayout.ForEach(Console.WriteLine);

            var hasChanges = true;

            while (hasChanges)
            {
                hasChanges = false;

                var newLayout = currentLayout.ConvertAll(l => new List<char>(l).ToArray());

                for (int row = 0; row < currentLayout.Count; row++)
                {
                    for (int col = 0; col < currentLayout[row].Length; col++)
                    {
                        var pos = currentLayout[row][col];

                        var occupiedSeats = checkAllVisibleSeats
                            ? GetNumberOfVisibleOccupiedSeats(currentLayout, row, col)
                            : GetNumberOfAdjacentOccupiedSeats(currentLayout, row, col);

                        if (pos == 'L' && occupiedSeats == 0)
                        {
                            newLayout[row][col] = '#';
                            hasChanges = true;
                        }
                        else if (pos == '#' && occupiedSeats > tolerance)
                        {
                            newLayout[row][col] = 'L';
                            hasChanges = true;
                        }
                        else
                        {
                            newLayout[row][col] = pos;
                        }
                    }
                }

                //Console.WriteLine(new string('=', newLayout[0].Length));
                //Console.WriteLine();
                //newLayout.ForEach(Console.WriteLine);
                //Console.WriteLine();

                currentLayout = newLayout.ConvertAll(cl => new List<char>(cl).ToArray()).ToList();
            }

            return currentLayout;
        }

        private static int GetNumberOfAdjacentOccupiedSeats(List<char[]> layout, int row, int col)
        {
            var count = 0;

            var isTopMost = row == 0;
            var isBottomMost = row == layout.Count - 1;
            var isLeftMost = col == 0;
            var isRightMost = col == layout[0].Length - 1;

            if (!isTopMost)
            {
                count += layout[row - 1][col] == '#' ? 1 : 0;
                count += !isLeftMost && layout[row - 1][col - 1] == '#' ? 1 : 0;
                count += !isRightMost && layout[row - 1][col + 1] == '#' ? 1 : 0;
            }

            if (!isLeftMost)
            {
                count += layout[row][col - 1] == '#' ? 1 : 0;
            }

            if (!isRightMost)
            {
                count += layout[row][col + 1] == '#' ? 1 : 0;
            }

            if (!isBottomMost)
            {
                count += layout[row + 1][col] == '#' ? 1 : 0;
                count += !isLeftMost && layout[row + 1][col - 1] == '#' ? 1 : 0;
                count += !isRightMost && layout[row + 1][col + 1] == '#' ? 1 : 0;
            }

            return count;
        }

        private static int GetNumberOfVisibleOccupiedSeats(List<char[]> layout, int row, int col)
        {
            var count = 0;

            for (int dirX = -1; dirX < 2; dirX++)
            {
                for (int dirY = -1; dirY < 2; dirY++)
                {
                    count += GetNumberOfVisibleOccupiedSeats(layout, row, col, dirX, dirY);
                }
            }

            return count;
        }

        private static int GetNumberOfVisibleOccupiedSeats(List<char[]> layout, int row, int col, int dirX, int dirY)
        {
            if (dirX == 0 && dirY == 0)
            {
                return 0;
            }

            var x = row + dirX;
            var y = col + dirY;

            while (x >= 0 && x < layout.Count && y >= 0 && y < layout[0].Length)
            {
                var pos = layout[x][y];

                if (pos == '#')
                {
                    return 1;
                }

                if (pos == 'L')
                {
                    return 0;
                }

                x += dirX;
                y += dirY;
            }

            return 0;
        }
    }
}

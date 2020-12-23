using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Puzzles
{
    public class Day12 : IPuzzle
    {
        public async Task Solve()
        {
            var inputData = (await InputDataReader.GetInputDataAsync<string>("Day12.txt")).ToList();

            Part1(inputData);
            Part2(inputData);
        }

        private static void Part1(List<string> inputData)
        {
            var ship = new Ship(0, 0, "E");

            foreach (var instruction in inputData)
            {
                //Console.WriteLine(ship.ToString());

                if (instruction.StartsWith("L") || instruction.StartsWith("R"))
                {
                    ship.Turn(instruction);
                }
                else
                {
                    ship.Move(instruction[..1], int.Parse(instruction[1..]));
                }
            }

            Console.WriteLine($"Part1: {Math.Abs(ship.X) + Math.Abs(ship.Y)}");
        }

        private static void Part2(List<string> inputData)
        {
            var ship = new Ship(0, 0, "E");

            foreach (var instruction in inputData)
            {
                //Console.WriteLine(ship.ToString());

                if (instruction.StartsWith("L") || instruction.StartsWith("R"))
                {
                    ship.TurnWaypoint(instruction[..1], int.Parse(instruction[1..]));
                }
                else if (instruction.StartsWith("F"))
                {
                    ship.MoveTowardsWaypoint(int.Parse(instruction[1..]));
                }
                else
                {
                    ship.MoveWaypoint(instruction[..1], int.Parse(instruction[1..]));
                }
            }

            Console.WriteLine($"Part2: {Math.Abs(ship.X) + Math.Abs(ship.Y)}");
        }

        private class Ship
        {
            private static readonly Dictionary<string, int> DirectionMap = new Dictionary<string, int>()
            {
                { "N", 0 },
                { "E", 90 },
                { "S", 180 },
                { "W", 270 }
            };

            public int X { get; set; }
            public int Y { get; set; }
            public string Direction { get; set; }
            public int WaypointX { get; set; }
            public int WaypointY { get; set; }

            public Ship(int x, int y, string direction)
            {
                X = x;
                Y = y;
                Direction = direction;

                WaypointX = 10;
                WaypointY = 1;
            }

            public void Move(string dir, int units)
            {
                switch (dir)
                {
                    case "N":
                        Y += units;
                        break;
                    case "E":
                        X += units;
                        break;
                    case "S":
                        Y -= units;
                        break;
                    case "W":
                        X -= units;
                        break;
                    case "F":
                        Move(Direction, units);
                        break;
                }
            }

            public void MoveTowardsWaypoint(int times)
            {
                for (int i = 0; i < times; i++)
                {
                    X += WaypointX;
                    Y += WaypointY;
                }
            }

            public void TurnWaypoint(string dir, int degrees)
            {
                for (int i = 0; i < degrees / 90; i++)
                {
                    int x, y;

                    if (dir == "L")
                    {
                        x = -WaypointY;
                        y = WaypointX;
                    }
                    else
                    {
                        x = WaypointY;
                        y = -WaypointX;
                    }

                    WaypointX = x;
                    WaypointY = y;
                }
            }

            public void MoveWaypoint(string dir, int units)
            {
                switch (dir)
                {
                    case "N":
                        WaypointY += units;
                        break;
                    case "E":
                        WaypointX += units;
                        break;
                    case "S":
                        WaypointY -= units;
                        break;
                    case "W":
                        WaypointX -= units;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(dir));
                }
            }

            public void Turn(string directionAndDegrees)
            {
                var degrees = int.Parse(directionAndDegrees[1..]);
                var turn = (directionAndDegrees.StartsWith("R") ? 1 : -1) * degrees;

                var oldDir = DirectionMap[Direction];
                var newDir = (oldDir == 0 && turn < 0 ? 360 : oldDir) + turn;

                if (newDir == 360)
                {
                    newDir = 0;
                }
                else if (newDir < 0)
                {
                    newDir += 360;
                }
                else if (newDir > 360)
                {
                    newDir -= 360;
                }

                Direction = DirectionMap.Single(d => d.Value == newDir).Key;
            }

            public override string ToString()
            {
                return $"({X}, {Y}) {Direction}";
            }
        }
    }
}

using AdventOfCode2020.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var puzzles = LoadPuzzles();

            var runPuzzleArg = args.Length == 1 ? args[0] : null;

            if (!string.IsNullOrWhiteSpace(runPuzzleArg))
            {
                var puzzleToSolve = puzzles.First(p => p.GetType().Name == runPuzzleArg);
                puzzles = new List<IPuzzle> { puzzleToSolve };
            }

            foreach (var puzzleToSolve in puzzles)
            {
                Console.WriteLine($"Solving {puzzleToSolve.GetType().Name} puzzle...");
                await puzzleToSolve.Solve();
                Console.WriteLine();
            }

            Console.WriteLine("All puzzles solved!");
            Console.ReadKey();
        }

        private static List<IPuzzle> LoadPuzzles()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IPuzzle).IsAssignableFrom(t) && !t.IsInterface)
                .Select(p => (IPuzzle)Activator.CreateInstance(p))
                .ToList();
        }
    }
}

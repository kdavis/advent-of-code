using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day3Tests
	{
		string ExampleInput = @"..##.........##.........##.........##.........##.........##.......
#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..
.#....#..#..#....#..#..#....#..#..#....#..#..#....#..#..#....#..#.
..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#
.#...##..#..#...##..#..#...##..#..#...##..#..#...##..#..#...##..#.
..#.##.......#.##.......#.##.......#.##.......#.##.......#.##.....
.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#
.#........#.#........#.#........#.#........#.#........#.#........#
#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...
#...##....##...##....##...##....##...##....##...##....##...##....#
.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#";

		public int TreesEncountered(string input, int right = 3, int down = 1)
		{
			int x = 0;
			int y = 0;

			var trees = 0;

			var lines = input.Split("\r\n");
			while (y + 1 < lines.Length)
			{
				x += right;
				y += down;

				if (lines[y][x % (lines[y].Length)] == '#')
				{
					trees++;
				}
			}

			return trees;
		}

		[TestMethod]
		public void TreeEncounterCountExample()
		{
			var encountered = TreesEncountered(ExampleInput);

			Assert.AreEqual(7, encountered);
		}

		[TestMethod]
		public async Task TreesEncounteredTask()
		{
			var input = await File.ReadAllTextAsync("Inputs/day3input.txt");

			var encountered = TreesEncountered(input);

			System.Console.WriteLine($"Encountered {encountered} trees");
		}

		[TestMethod]
		public void TreesBestUnencounteredExample()
		{
			var attemptedDirections = new []
			{
				(1, 1),
				(3, 1),
				(5, 1),
				(7, 1),
				(1, 2)
			};

			var differentValues = attemptedDirections
				.Select(r => TreesEncountered(ExampleInput, right: r.Item1, down: r.Item2));

			var product = 1;
			foreach (var value in differentValues)
            {
				product *= value;
            }

			Assert.AreEqual(product, 336);
		}

		[TestMethod]
		public async Task TreesBestUnencounteredTask()
		{
			var input = await File.ReadAllTextAsync("Inputs/day3input.txt");

			var attemptedDirections = new[]
			{
				(1, 1),
				(3, 1),
				(5, 1),
				(7, 1),
				(1, 2)
			};

			var differentValues = attemptedDirections
				.Select(r => TreesEncountered(input, right: r.Item1, down: r.Item2));

			Int64 product = 1;
			foreach (var value in differentValues)
			{
				product = product * value;
			}

			System.Console.WriteLine($"Product of encountered trees: {product}");
		}
	}
}

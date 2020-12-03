using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day3Tests
	{
		public int TreesEncountered(string input)
		{
			int x = 0;
			int y = 0;

			var trees = 0;

			var lines = input.Split("\r\n");
			while (y + 1 < lines.Length)
			{
				x += 3;
				y += 1;

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
			var input = @"..##.........##.........##.........##.........##.........##.......  --->
#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..
.#....#..#..#....#..#..#....#..#..#....#..#..#....#..#..#....#..#.
..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#
.#...##..#..#...##..#..#...##..#..#...##..#..#...##..#..#...##..#.
..#.##.......#.##.......#.##.......#.##.......#.##.......#.##.....  --->
.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#
.#........#.#........#.#........#.#........#.#........#.#........#
#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...
#...##....##...##....##...##....##...##....##...##....##...##....#
.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#  --->";

			var encountered = TreesEncountered(input);

			Assert.AreEqual(7, encountered);
		}

		[TestMethod]
		public async Task TreesEncounteredTask()
		{
			var input = await File.ReadAllTextAsync("Inputs/day3input.txt");

			var encountered = TreesEncountered(input);

			System.Console.WriteLine($"Encountered {encountered} trees");
		}
	}
}

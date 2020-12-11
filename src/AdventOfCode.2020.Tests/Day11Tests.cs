using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day11Tests
	{
		public string SeatFinder(string input)
		{
			var seats = input.Split("\r\n").Select(q => q.ToList()).ToList();
			var takenSeats = new List<List<char>>(seats.Select(q => new List<char>(q)));

			for (var row = 0; row < seats.Count; row++)
			{
				for (var column = 0; column < seats[row].Count; column++)
				{
					var surroundingSeats = seats
							.Select((rO, rI) =>
								rI - 1 <= row && row <= rI + 1 ?
								rO.Select((cO, cI) =>
									cI - 1 <= column && column <= cI + 1 && !(column == cI && row == rI) ?
									(char?)cO :
									null
								) :
								null
							);
					if (seats[row][column] == 'L')
					{
						var isGood = surroundingSeats
							.Where(q => q != null)
							.All(rowO => rowO
								.Where(colO => colO != null)
								.All(colO => colO != '#')
							);

						if (isGood)
							takenSeats[row][column] = '#';
					}
					else if (seats[row][column] == '#')
					{
						var surroundingTakenSeatCount = surroundingSeats
							.Where(q => q != null)
							.Sum(rowO => rowO
								.Where(colO => colO != null)
								.Count(colO => colO == '#')
							);

						if (surroundingTakenSeatCount >= 4)
							takenSeats[row][column] = 'L';
					}
				}
			}

			return string.Join("\r\n", takenSeats.Select(q => string.Join("", q)));
		}

		public char RaytraceDirection(List<List<char>> seats, int directionX, int directionY, int startX, int startY)
		{
			if (directionX == 0 && directionY == 0)
			{
				return '.';
			}

			var x = startX + directionX;
			var y = startY + directionY;

			try
			{
				while (seats[x][y] == '.')
				{
					x += directionX;
					y += directionY;
				}
			}
			catch (Exception)
			{
				return '.';
			}

			return seats[x][y];
		}

		public string RaytraceSeatFinder(string input)
		{
			var seats = input.Split("\r\n").Select(q => q.ToList()).ToList();
			var takenSeats = new List<List<char>>(seats.Select(q => new List<char>(q)));

			for (var row = 0; row < seats.Count; row++)
			{
				for (var column = 0; column < seats[row].Count; column++)
				{
					var surroundings = new List<char>();
					for (var diffX = -1; diffX <= 1; diffX++)
					{
						for (var diffY = -1; diffY <= 1; diffY++)
						{
							surroundings.Add(RaytraceDirection(seats, diffX, diffY, row, column));
						}
					}

					if (seats[row][column] == '#' && surroundings.Count(q => q == '#') >= 5)
					{
						takenSeats[row][column] = 'L';
					}
					if (seats[row][column] == 'L' && surroundings.Count(q => q == '#') == 0)
					{
						takenSeats[row][column] = '#';
					}
				}
			}

			return string.Join("\r\n", takenSeats.Select(q => string.Join("", q)));
		}

		[TestMethod]
		public void SeatFinderTest()
		{
			var foundSeat = SeatFinder(@"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL");

			Assert.AreEqual(@"#.##.##.##
#######.##
#.#.#..#..
####.##.##
#.##.##.##
#.#####.##
..#.#.....
##########
#.######.#
#.#####.##", foundSeat);
		}

		[TestMethod]
		public void SeatFinderTest_Stage2()
		{
			var foundSeat = SeatFinder(@"#.##.##.##
#######.##
#.#.#..#..
####.##.##
#.##.##.##
#.#####.##
..#.#.....
##########
#.######.#
#.#####.##");

			Assert.AreEqual(@"#.LL.L#.##
#LLLLLL.L#
L.L.L..L..
#LLL.LL.L#
#.LL.LL.LL
#.LLLL#.##
..L.L.....
#LLLLLLLL#
#.LLLLLL.L
#.#LLLL.##", foundSeat);
		}

		[TestMethod]
		public void SeatFinderTest_Stage3()
		{
			var foundSeat = SeatFinder(@"#.LL.L#.##
#LLLLLL.L#
L.L.L..L..
#LLL.LL.L#
#.LL.LL.LL
#.LLLL#.##
..L.L.....
#LLLLLLLL#
#.LLLLLL.L
#.#LLLL.##");

			Assert.AreEqual(@"#.##.L#.##
#L###LL.L#
L.#.#..#..
#L##.##.L#
#.##.LL.LL
#.###L#.##
..#.#.....
#L######L#
#.LL###L.L
#.#L###.##", foundSeat);
		}

		[TestMethod]
		public void SeatFinderTest_Stage4()
		{
			var foundSeat = SeatFinder(@"#.##.L#.##
#L###LL.L#
L.#.#..#..
#L##.##.L#
#.##.LL.LL
#.###L#.##
..#.#.....
#L######L#
#.LL###L.L
#.#L###.##");

			Assert.AreEqual(@"#.#L.L#.##
#LLL#LL.L#
L.L.L..#..
#LLL.##.L#
#.LL.LL.LL
#.LL#L#.##
..L.L.....
#L#LLLL#L#
#.LLLLLL.L
#.#L#L#.##", foundSeat);
		}


		[TestMethod]
		public void SeatFinderTest_Stage5()
		{
			var foundSeat = SeatFinder(@"#.#L.L#.##
#LLL#LL.L#
L.L.L..#..
#LLL.##.L#
#.LL.LL.LL
#.LL#L#.##
..L.L.....
#L#LLLL#L#
#.LLLLLL.L
#.#L#L#.##");

			Assert.AreEqual(@"#.#L.L#.##
#LLL#LL.L#
L.#.L..#..
#L##.##.L#
#.#L.LL.LL
#.#L#L#.##
..L.L.....
#L#L##L#L#
#.LLLLLL.L
#.#L#L#.##", foundSeat);
		}


		[TestMethod]
		public void SeatFinderTest_Stage6()
		{
			var foundSeat = SeatFinder(@"#.#L.L#.##
#LLL#LL.L#
L.#.L..#..
#L##.##.L#
#.#L.LL.LL
#.#L#L#.##
..L.L.....
#L#L##L#L#
#.LLLLLL.L
#.#L#L#.##");

			Assert.AreEqual(@"#.#L.L#.##
#LLL#LL.L#
L.#.L..#..
#L##.##.L#
#.#L.LL.LL
#.#L#L#.##
..L.L.....
#L#L##L#L#
#.LLLLLL.L
#.#L#L#.##", foundSeat);
		}

		[TestMethod]
		public async Task SeatFinderTask()
		{
			var input = await File.ReadAllTextAsync("Inputs/day11input.txt");

			var prevGrid = input;
			while (true)
			{
				var newGrid = SeatFinder(prevGrid);
				if (prevGrid == newGrid)
				{
					break;
				}
				prevGrid = newGrid;
			}

			Console.WriteLine($"There are {prevGrid.Where(q => q == '#').Count()} occupied seats");
		}

		[TestMethod]
		public async Task RaytraceSeatFinderTask()
		{
			var input = await File.ReadAllTextAsync("Inputs/day11input.txt");

			var prevGrid = input;
			while (true)
			{
				var newGrid = RaytraceSeatFinder(prevGrid);
				if (prevGrid == newGrid)
				{
					break;
				}
				prevGrid = newGrid;
			}

			Console.WriteLine($"There are {prevGrid.Where(q => q == '#').Count()} occupied seats");
		}
	}
}
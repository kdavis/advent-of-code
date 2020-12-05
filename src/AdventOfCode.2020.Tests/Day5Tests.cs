using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day5Tests
	{
		string ExampleInput = @"";

		public (int, int, int) SeatGuesser(string input)
		{
			(int, int) rowRange = (0, 127);
			(int, int) columnRange = (0, 7);

			int rowSeatAverage() => rowRange.Item1 + (int)Math.Ceiling((decimal)(rowRange.Item2 - rowRange.Item1) / 2);
			int columnSeatAverage() => columnRange.Item1 + (int)Math.Ceiling((decimal)(columnRange.Item2 - columnRange.Item1) / 2);

			input.ToList().ForEach(character =>
			{
				switch (character)
				{
					case 'F':
						rowRange.Item2 = rowSeatAverage();
						break;
					case 'B':
						rowRange.Item1 = rowSeatAverage();
						break;
					case 'L':
						columnRange.Item2 = columnSeatAverage();
						break;
					case 'R':
						columnRange.Item1 = columnSeatAverage();
						break;
				}
			});

			return (rowRange.Item1, columnRange.Item1, (rowRange.Item1 * 8) + columnRange.Item1);
		}

		[TestMethod]
		public void SeatGuesserTest()
		{
			var seat = SeatGuesser("FBFBBFFRLR");

			Assert.AreEqual(44, seat.Item1);
			Assert.AreEqual(5, seat.Item2);
			Assert.AreEqual(357, seat.Item3);

			seat = SeatGuesser("BFFFBBFRRR");

			Assert.AreEqual(70, seat.Item1);
			Assert.AreEqual(7, seat.Item2);
			Assert.AreEqual(567, seat.Item3);

			seat = SeatGuesser("FFFBBBFRRR");

			Assert.AreEqual(14, seat.Item1);
			Assert.AreEqual(7, seat.Item2);
			Assert.AreEqual(119, seat.Item3);

			seat = SeatGuesser("BBFFBBFRLL");

			Assert.AreEqual(102, seat.Item1);
			Assert.AreEqual(4, seat.Item2);
			Assert.AreEqual(820, seat.Item3);
		}

		[TestMethod]
		public async Task SeatGuesserTask()
		{
			var input = await File.ReadAllLinesAsync("Inputs/day5input.txt");

			Console.WriteLine(input.Select(line => SeatGuesser(line)).Max(seat => seat.Item3));
		}
	}
}

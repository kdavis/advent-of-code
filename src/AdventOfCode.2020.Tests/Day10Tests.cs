using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day10Tests
	{
		public class JoltageAdapterList
		{
			public int Jolt1Diffs { get; set; }
			public int Jolt3Diffs { get; set; }
			public List<int> Adapters { get; set; }
		}

		public JoltageAdapterList JoltCalculator(string[] input)
		{
			var numbers = input.Select(number => int.Parse(number));

			var maxDeviceRated = numbers.Max() + 3;

			var minJoltage = numbers.Min();

			int jolt1Diff = 0, jolt3Diff = 0;

			var joltageAdapters = new List<int>();

			for (int currentJoltage = 0; currentJoltage < maxDeviceRated;)
			{
				var nextJoltageAdapter = numbers
					.Where(number => number > currentJoltage && number <= currentJoltage + 3)
					.OrderBy(q => q)
					.FirstOrDefault();

				if (nextJoltageAdapter == 0)
				{
					nextJoltageAdapter = maxDeviceRated;
				}

				joltageAdapters.Add(currentJoltage);

				if (nextJoltageAdapter - currentJoltage == 1)
					jolt1Diff++;
				else if (nextJoltageAdapter - currentJoltage == 3)
					jolt3Diff++;

				currentJoltage = nextJoltageAdapter;
			}

			return new JoltageAdapterList
			{
				Adapters = joltageAdapters,
				Jolt1Diffs = jolt1Diff,
				Jolt3Diffs = jolt3Diff
			};
		}

		[TestMethod]
		public void JoltCalculatorTest()
		{
			var joltCalculated = JoltCalculator(@"16
10
15
5
1
11
7
19
6
12
4".Split("\r\n"));

			Assert.AreEqual(5 * 7, joltCalculated.Jolt1Diffs * joltCalculated.Jolt3Diffs);
		}

		[TestMethod]
		public async Task JoltCalculatorTask()
		{
			var input = await File.ReadAllLinesAsync("Inputs/day10input.txt");

			var joltCalculated = JoltCalculator(input);

			Console.WriteLine($"The product of 1 jolt diffs and 3 jolt diffs is {joltCalculated.Jolt1Diffs * joltCalculated.Jolt3Diffs}");
		}

	}
}
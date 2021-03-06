using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day9Tests
	{
		public long CrackXMAS(string[] input, int preambleLength)
		{
			var numbers = input.Select(number => long.Parse(number));

			for (int i = preambleLength; i < numbers.Count(); i++)
			{
				var valuesPreamble = numbers.Skip(i - preambleLength).Take(preambleLength);

				var valueIsGood = false;

				for (var j = 0; j < valuesPreamble.Count(); j++)
				{
					for (var k = j + 1; k < valuesPreamble.Count(); k++)
					{
						if (valuesPreamble.ElementAt(j) + valuesPreamble.ElementAt(k) == numbers.ElementAt(i))
						{
							valueIsGood = true;
						}
					}
				}

				if (!valueIsGood)
					return numbers.ElementAt(i);
			}

			return 0;
		}

		public long GetListOfXMASCrack(string[] input, int preambleLength)
		{
			var numbers = input.Select(number => long.Parse(number));

			var crackedNumber = CrackXMAS(input, preambleLength);

			for (int offset = 0; offset < numbers.Count(); offset++)
			{
				for (int i = 2; i < numbers.Count(); i++)
				{
					var foundPreamble = numbers.Skip(offset).Take(i);
					if (foundPreamble.Sum() == crackedNumber)
                    {
						return foundPreamble.Min() + foundPreamble.Max();
					}
				}
			}

			return 0;
		}

		[TestMethod]
		public void CrackXMASTest()
		{
			var nonConforming = CrackXMAS(@"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576".Split("\r\n"), 5);

			Assert.AreEqual(127, nonConforming);
		}

		[TestMethod]
		public async Task CrackXMASTask()
		{
			var input = await File.ReadAllLinesAsync("Inputs/day9input.txt");

			var nonConforming = CrackXMAS(input, 25);

			Console.WriteLine($"The value that is non-conforming is {nonConforming}");
		}

		[TestMethod]
		public void SumPreambleCrackXMASTest()
		{
			var nonConforming = GetListOfXMASCrack(@"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576".Split("\r\n"), 5);

			Assert.AreEqual(62, nonConforming);
		}

		[TestMethod]
		public async Task SumPreambleCrackXMASTask()
		{
			var input = await File.ReadAllLinesAsync("Inputs/day9input.txt");

			var nonConforming = GetListOfXMASCrack(input, 25);

			Console.WriteLine($"The value of min+max of the preamble to the non-conforming is {nonConforming}");
		}
	}
}
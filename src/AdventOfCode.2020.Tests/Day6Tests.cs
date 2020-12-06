using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day6Tests
	{
		public List<string> GroupAnswerIdentifier(string input)
		{
			var groupOfGroups = input.Split("\r\n\r\n")
				.Select(group => string.Join("", group.Replace("\r\n", "").ToList().Distinct())).ToList();

			return groupOfGroups;
		}

		[TestMethod]
		public void GroupAnswerIdentifierTest()
		{
			var groupOfGroups = GroupAnswerIdentifier(@"abc

a
b
c

ab
ac

a
a
a
a

b");

			Assert.AreEqual(3, groupOfGroups[0].Length);
			Assert.AreEqual(3, groupOfGroups[1].Length);
			Assert.AreEqual(3, groupOfGroups[2].Length);
			Assert.AreEqual(1, groupOfGroups[3].Length);
			Assert.AreEqual(1, groupOfGroups[4].Length);
			Assert.AreEqual(11, groupOfGroups.Sum(group => group.Length));
		}

		[TestMethod]
		public async Task GroupAnswerIdentifierTask()
		{
			var input = await File.ReadAllTextAsync("Inputs/day6input.txt");

			var groupOfGroups = GroupAnswerIdentifier(input);

			Console.WriteLine($"Sum of distinct group answers: {groupOfGroups.Sum(group => group.Length)}");

		}
	}
}
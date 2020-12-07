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
		public List<string> UniqueAnswerIdentifier(string input)
		{
			var groupOfGroups = input.Split("\r\n\r\n")
				.Select(group => string.Join("", group.Replace("\r\n", "").ToList().Distinct())).ToList();

			return groupOfGroups;
		}

		public List<List<char>> GroupAnswerIdentifier(string input)
		{
			var groups = input.Split("\r\n\r\n");

			var distinctGroupAnswers = new List<List<char>>();

			foreach (var group in groups)
			{
				var answers = group.Split("\r\n").Where(q => !string.IsNullOrEmpty(q));

				var allCharacters = answers.SelectMany(q => q.ToList()).Distinct();

				var charactersFoundInAnswers = allCharacters.Where(character =>
				{
					return answers.All(answer => answer.Contains(character));
				}).ToList();

				distinctGroupAnswers.Add(charactersFoundInAnswers);
			}

			return distinctGroupAnswers;
		}

		[TestMethod]
		public void UniqueAnswerIdentifierTest()
		{
			var groupOfGroups = UniqueAnswerIdentifier(@"abc

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
		public async Task UniqueAnswerIdentifierTask()
		{
			var input = await File.ReadAllTextAsync("Inputs/day6input.txt");

			var groupOfGroups = UniqueAnswerIdentifier(input);

			Console.WriteLine($"Sum of distinct group answers: {groupOfGroups.Sum(group => group.Length)}");
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

			Assert.AreEqual(3, groupOfGroups[0].Count);
			Assert.AreEqual(0, groupOfGroups[1].Count);
			Assert.AreEqual(1, groupOfGroups[2].Count);
			Assert.AreEqual(1, groupOfGroups[3].Count);
			Assert.AreEqual(1, groupOfGroups[4].Count);
			Assert.AreEqual(6, groupOfGroups.Sum(group => group.Count));
		}

		[TestMethod]
		public async Task GroupAnswerIdentifierTask()
		{
			var input = await File.ReadAllTextAsync("Inputs/day6input.txt");

			var groupOfGroups = GroupAnswerIdentifier(input);

			Console.WriteLine($"Sum of all answered distinct group answers: {groupOfGroups.Sum(group => group.Count)}");
		}
	}
}
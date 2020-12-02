using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day2Tests
	{
		[TestMethod]
		public async Task SledRentalValidPasswordsCount()
		{
			var inputs = await File.ReadAllLinesAsync("Inputs/day2input.txt");

			var validPasswords = 0;

			foreach (var input in inputs)
			{
				var rangeEndIndex = input.IndexOf(" ");
				var requirementsEndIndex = input.IndexOf(": ");

				var range = input.Substring(0, rangeEndIndex);
				var requiredLetter = input.Substring(rangeEndIndex + 1, requirementsEndIndex - (rangeEndIndex + 1));

				var ranges = range.Split("-");
				var minCountRequirement = int.Parse(ranges[0]);
				var maxCountRequirement = int.Parse(ranges[1]);

				var password = input.Substring(requirementsEndIndex + 2);
				var letterExistCount = password.Where(letter => letter.ToString() == requiredLetter).Count();

				if ((minCountRequirement <= letterExistCount) && (letterExistCount <= maxCountRequirement))
				{
					validPasswords++;
				}
			}

			System.Console.WriteLine($"There are {validPasswords} passwords valid at the sled rental shop down the street.");
		}

		[TestMethod]
		public async Task TobogganValidPasswordsCount()
		{
			var inputs = await File.ReadAllLinesAsync("Inputs/day2input.txt");

			var validPasswords = 0;

			foreach (var input in inputs)
			{
				var rangeEndIndex = input.IndexOf(" ");
				var requirementsEndIndex = input.IndexOf(": ");

				var indexRange = input.Substring(0, rangeEndIndex);
				var requiredLetter = char.Parse(input.Substring(rangeEndIndex + 1, requirementsEndIndex - (rangeEndIndex + 1)));

				var indexes = indexRange.Split("-");
				var firstIndexRequirement = int.Parse(indexes[0]);
				var secondIndexRequirement = int.Parse(indexes[1]);

				var password = input.Substring(requirementsEndIndex + 2);

				var passwordFirstCharAt = password[firstIndexRequirement - 1];
				var passwordSecondCharAt = password[secondIndexRequirement - 1];

				if (passwordFirstCharAt == requiredLetter ^ passwordSecondCharAt == requiredLetter)
				{
					validPasswords++;
				}
			}
			System.Console.WriteLine($"There are {validPasswords} passwords valid for the Official Toboggan Corporate Authentication System.");
		}
	}
}

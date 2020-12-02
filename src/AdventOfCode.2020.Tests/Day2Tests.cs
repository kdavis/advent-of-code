using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day2Tests
	{
		[TestMethod]
		public async Task ValidPasswordsCount()
		{
			var inputs = await File.ReadAllLinesAsync("Inputs/day2input.txt");

			var validPasswords = 0;

			// 8-16 v: vfvvhvvtvvvjdvvfvv
			foreach (var input in inputs)
			{
				var rangeEndIndex = input.IndexOf(" ");
				var requirementsEndIndex = input.IndexOf(": ");

				var range = input.Substring(0, rangeEndIndex);
				var requiredLetter = input.Substring(rangeEndIndex + 1, requirementsEndIndex - (rangeEndIndex + 1));

				var ranges = range.Split("-");
				var minCountRequirement = int.Parse(ranges[0]);
				var maxCountRequirement = int.Parse(ranges[1]);

				var password = input.Substring(requirementsEndIndex + 1);
				var letterExistCount = password.Where(letter => letter.ToString() == requiredLetter).Count();

				if ((minCountRequirement <= letterExistCount) && (letterExistCount <= maxCountRequirement))
				{
					validPasswords++;
				}
			}

			System.Console.WriteLine($"There are {validPasswords} passwords valid.");
		}
	}
}

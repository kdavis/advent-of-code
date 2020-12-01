using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day1Tests
	{
		[TestMethod]
		public async Task ProductOfTwoEntriesThatSumTo2020()
		{
			var inputs = await File.ReadAllLinesAsync("Inputs/day1input.txt");

			var expenses = inputs.Select(input => int.Parse(input)).ToList();

			var expense = expenses.SelectMany(
				expense1 => expenses.Select(
						expense2 => new
						{
							Expense1 = expense1,
							Expense2 = expense2,
							Sum = expense1 + expense2,
							Product = expense1 * expense2
						})
				)
				.Where(a => a.Sum == 2020)
				.FirstOrDefault();

			System.Console.WriteLine($"Product of two entries that sum to 2020: {expense.Product}");
		}

		[TestMethod]
		public async Task ProductOfThreeEntriesThatSumTo2020()
		{
			var inputs = await File.ReadAllLinesAsync("Inputs/day1input.txt");

			var expenses = inputs.Select(input => int.Parse(input)).ToList();

			var expense = expenses.SelectMany(
				expense1 => expenses.SelectMany(
					expense2 => expenses.Select(
						expense3 => new
						{
							Expense1 = expense1,
							Expense2 = expense2,
							Expense3 = expense3,
							Sum = expense1 + expense2 + expense3,
							Product = expense1 * expense2 * expense3
						})
					)
				)
				.Where(a => a.Sum == 2020)
				.FirstOrDefault();

			System.Console.WriteLine($"Product of three entries that sum to 2020: {expense.Product}");
		}
	}
}

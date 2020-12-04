using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day4Tests
	{
		string ExampleInput = @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in";

		public int PassportsValidCount(string input)
		{
			var passports = input.Split("\r\n\r\n");

			var requiredFields = new string[]
			{
				"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"
			};

			var valid = 0;

			foreach (var passport in passports)
			{
				var fields = passport.Split(new string[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries);

				var validFieldCount = fields.Count(
					field => requiredFields.Contains(field.Split(":")[0])
				);

				if (validFieldCount == requiredFields.Length)
				{
					valid++;
				}
			}

			return valid;
		}

		[TestMethod]
		public void PassportValidCountTest()
		{
			var validCount = PassportsValidCount(ExampleInput);

			Assert.AreEqual(2, validCount);
		}


		[TestMethod]
		public async Task PassportValidCountTask()
		{
			var input = await File.ReadAllTextAsync("Inputs/day4input.txt");

			var validCount = PassportsValidCount(input);

			System.Console.WriteLine($"There are {validCount} valid passports");
		}

	}
}

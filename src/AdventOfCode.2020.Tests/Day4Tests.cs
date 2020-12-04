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

		public int PassportsStrictValidCount(string input)
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

				var validFieldCount = 0;

				var isPasswordValid = fields.All(field =>
				{
					var split = field.Split(":");

					if (requiredFields.Contains(split[0]))
						validFieldCount++;

					switch (split[0])
					{
						case "byr":
							var birthYear = int.Parse(split[1]);
							return 1920 <= birthYear && birthYear <= 2002;
						case "iyr":
							var issueYear = int.Parse(split[1]);
							return 2010 <= issueYear && issueYear <= 2020;
						case "eyr":
							var expiryYear = int.Parse(split[1]);
							return 2020 <= expiryYear && expiryYear <= 2030;
						case "hgt":
							var inches = split[1].EndsWith("in");
							var cms = split[1].EndsWith("cm");
							if (!cms && !inches)
							{
								return false;
							}
							var heightNumbers = split[1].Substring(0, split[1].Length - 2);
							var height = int.Parse(heightNumbers);

							return
								(inches && (59 <= height && height <= 76)) ||
								(cms && (150 <= height && height <= 193));
						case "hcl":
							return split[1].Length == 7 &&
								split[1].StartsWith("#") &&
								split[1].Substring(1).All(value =>
									char.IsDigit(value) ||
									"abcdef".Contains(value.ToString())
								);
						case "ecl":
							return new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(
								split[1]
							);
						case "pid":
							return split[1].All(number => char.IsDigit(number)) && split[1].Length == 9;
						default:
							return true;
					}
				});

				if (isPasswordValid && validFieldCount == requiredFields.Length)
					valid++;
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


		[TestMethod]
		public async Task PassportExtraValidCountTask()
		{
			var input = await File.ReadAllTextAsync("Inputs/day4input.txt");

			var validCount = PassportsStrictValidCount(input);

			System.Console.WriteLine($"There are {validCount} valid passports");
		}

	}
}

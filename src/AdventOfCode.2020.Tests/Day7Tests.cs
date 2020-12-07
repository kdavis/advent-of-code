using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day7Tests
	{
		public class BagHold
        {
			public int Count { get; set; }
			public string Colour { get; set; }
        }

		public class BagRule
        {
			public string Colour { get; set; }
			public List<BagHold> BagHolds { get; set; }
        }

		public List<BagRule> BaggageRules(string[] input)
		{
			var ruleConfigurations = input.Select(rule =>
			{
				var ruleSide = rule.Split(" bags contain ");
				var otherColours = ruleSide[1][..^1].Split(", ");
				return new BagRule
				{
					Colour = ruleSide[0],
					BagHolds = ruleSide[1][..^1] == "no other bags" ? new List<BagHold>() : ruleSide[1][..^1].Split(", ").Select(colour =>
                    {
						return new BagHold
						{
							Count = int.Parse(colour[..colour.IndexOf(" ")]),
							Colour = colour[colour.IndexOf(" ")..colour.IndexOf(" bag")]
						};
                    }).ToList()
				};
			}).ToList();

			return ruleConfigurations;
		}

		public int CountCanHold(string searchBagColour, BagRule currentBagRule, List<BagRule> baggageRules)
        {
			int count = 0;
			foreach (var hold in currentBagRule.BagHolds)
            {
				var thisColour = hold.Colour.Trim();
				if (thisColour == searchBagColour)
                {
					count += hold.Count;
                }
				else
                {
					count += CountCanHold(searchBagColour, baggageRules.Where(q => q.Colour == thisColour).FirstOrDefault(), baggageRules);
				}
            }
			return count;
        }

		[TestMethod]
		public void BagCountainsTest()
		{
			var baggageRules = BaggageRules(@"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.".Split("\r\n"));

			int count = 0;

			foreach (var bagRule in baggageRules)
			{
				if (CountCanHold("shiny gold", bagRule, baggageRules) > 0)
				{
					count++;
				}
			}

			Assert.AreEqual(4, count);
		}

		[TestMethod]
		public async Task BagCountainsTask()
		{
			var input = await File.ReadAllLinesAsync("Inputs/day7input.txt");
			var colour = "shiny gold";

			var baggageRules = BaggageRules(input);

			int count = 0;

			foreach (var bagRule in baggageRules)
			{
				if (CountCanHold(colour, bagRule, baggageRules) > 0)
				{
					count++;
				}
			}

            Console.WriteLine($"Amount of bags that can hold a {colour} bag are: {count}");
		}
	}
}
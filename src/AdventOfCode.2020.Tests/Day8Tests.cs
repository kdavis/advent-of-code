using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
	[TestClass]
	public class Day8Tests
	{
		public int FindBootLoopInstruction(string[] inputs)
		{
			var completedInstructionIndexes = new List<int>();
			var lastInstructionIndex = 0;

			var accumulator = 0;

			for (int i = 0; i < inputs.Length; i++)
			{
				if (completedInstructionIndexes.Contains(lastInstructionIndex))
				{
					return accumulator;
				}
				completedInstructionIndexes.Add(lastInstructionIndex);

				var opcode = inputs[lastInstructionIndex].Split(" ");
				var action = opcode[0];

				var operand = int.Parse(opcode[1]);

				switch (action)
				{
					case "nop":
						lastInstructionIndex++;
						break;
					case "jmp":
						lastInstructionIndex += operand;
						break;
					case "acc":
						lastInstructionIndex++;
						accumulator += operand;
						break;
				}
			}

			return accumulator;
		}

		public int FixBootLoopInstruction(string[] inputs)
		{
			for (var x = 0; x < inputs.Length; x++)
			{
				var completedInstructionIndexes = new List<int>();
				var lastInstructionIndex = 0;

				var accumulator = 0;

				for (int i = 0; i < inputs.Length; i++)
				{
					if (lastInstructionIndex > inputs.Length - 1)
					{
						return accumulator;
					}
					completedInstructionIndexes.Add(lastInstructionIndex);

					var opcode = inputs[lastInstructionIndex].Split(" ");
					var action = opcode[0];

					if (lastInstructionIndex == x)
					{
						action = action == "nop" ? "jmp" : "nop";
					}

					var operand = int.Parse(opcode[1]);

					switch (action)
					{
						case "nop":
							lastInstructionIndex++;
							break;
						case "jmp":
							lastInstructionIndex += operand;
							break;
						case "acc":
							lastInstructionIndex++;
							accumulator += operand;
							break;
					}
				}
			}

			return 0;
		}

		[TestMethod]
		public void BootCodeLoopTest()
		{
			var accumulator = FindBootLoopInstruction(@"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6".Split("\r\n"));

			Assert.AreEqual(5, accumulator);
		}

		[TestMethod]
		public async Task BootCodeLoopTask()
		{
			var input = await File.ReadAllLinesAsync("Inputs/day8input.txt");

			var accumulator = FindBootLoopInstruction(input);

			Console.WriteLine($"The value in the accumulator before the boot loop is {accumulator}");
		}

		[TestMethod]
		public void BootCodeFixTest()
		{
			var accumulator = FixBootLoopInstruction(@"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6".Split("\r\n"));

			Assert.AreEqual(8, accumulator);
		}

		[TestMethod]
		public async Task BootCodeFixTask()
		{
			var input = await File.ReadAllLinesAsync("Inputs/day8input.txt");

			var accumulator = FixBootLoopInstruction(input);

			Console.WriteLine($"The value in the accumulator after the boot loop fix is {accumulator}");
		}
	}
}
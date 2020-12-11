using AdventOfCode.DTO;
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day08
{
    [ExpectedResult("1125")]
    public class Task2 : IRunnableCode
    {
        private const string _accumulator = "acc";
        private const string _jumps = "jmp";
        private const string _noOperation = "nop";

        private const char _instructionPartDelimiter = ' ';

        public async Task<string> ExecuteAsync(string[] data)
        {
            var instructions = data.Select(x => new Instruction(x, _instructionPartDelimiter)).ToList();

            _ = RunProgram(instructions, out var accumulator);

            for (var i = 0; i < instructions.Count; i++)
            {
                instructions.ForEach(x => x.Step = 0);

                var operation = instructions[i].Operation;

                instructions[i].Operation = operation switch
                {
                    _jumps => _noOperation,
                    _noOperation => _jumps,
                    _ => _accumulator
                };

                accumulator = 0;

                if (RunProgram(instructions, out accumulator))
                {
                    return accumulator.ToString();
                }

                instructions[i].Operation = operation;
            }

            return "Failed!";
        }

        private static bool RunProgram(IList<Instruction> instructions, out int accumulator)
        {
            var step = 0;
            var localAccumulator = 0;

            for (var i = 0; i < instructions.Count; i++)
            {
                var instruction = instructions[i];

                ++step;

                if (instruction.Step != 0)
                {
                    accumulator = localAccumulator;
                    return false;
                }
                else
                {
                    instruction.Step = step;
                }

                switch (instruction.Operation)
                {
                    case _accumulator:
                        localAccumulator += instruction.Increment;
                        break;
                    case _jumps:
                        i += instruction.Increment - 1;

                        break;
                    case _noOperation:
                    default:
                        break;
                }
            }

            accumulator = localAccumulator;
            return true;
        }
    }
}
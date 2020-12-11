using AdventOfCode.DTO;
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day08
{
    [ExpectedResult("1137")]
    public class Task1 : IRunnableCode
    {
        private const string _accumulator = "acc";
        private const string _jumps = "jmp";
        private const string _noOperation = "nop";

        private const char _instructionPartDelimiter = ' ';

        public async Task<string> ExecuteAsync(string[] data)
        {
            var accumulator = 0;
           
            var instructions = data.Select(x => new Instruction(x , _instructionPartDelimiter)).ToList();

            accumulator = RunProgram(instructions);

            return accumulator.ToString();
        }

        private static int RunProgram(IList<Instruction> instructions)
        {
            var step = 0;
            var accumulator = 0;

            for (var i = 0; i < instructions.Count; i++)
            {
                var instruction = instructions[i];

                ++step;

                if (instruction.Step != 0)
                {
                    return accumulator;
                }
                else
                {
                    instruction.Step = step;
                }

                switch (instruction.Operation)
                {
                    case _accumulator:
                        accumulator += instruction.Increment;
                        break;
                    case _jumps:
                        i += instruction.Increment - 1;

                        break;
                    case _noOperation:
                    default:
                        break;
                }
            }

            return accumulator;
        }
    }
}
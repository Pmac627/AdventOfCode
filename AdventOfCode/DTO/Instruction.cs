namespace AdventOfCode.DTO
{
    public class Instruction
    {
        private const char _instructionPartDelimiter = ' ';

        public Instruction(string instruction, char instructionPartDelimiter = _instructionPartDelimiter)
        {
            var parts = instruction.Split(instructionPartDelimiter);

            Operation = parts[0];

            if (int.TryParse(parts[1], out var inc))
            {
                Increment = inc;
            }
        }

        public string Operation { get; set; }
        public int Increment { get; set; }
        public int Step { get; set; }
    }
}
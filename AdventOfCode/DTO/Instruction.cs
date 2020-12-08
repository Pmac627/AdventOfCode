namespace AdventOfCode.DTO
{
    public class Instruction
    {
        public Instruction(string operation, string increment)
        {
            Operation = operation;

            if (int.TryParse(increment, out var inc))
            {
                Increment = inc;
            }
        }

        public string Operation { get; set; }
        public int Increment { get; set; }
        public int Step { get; set; }
    }
}
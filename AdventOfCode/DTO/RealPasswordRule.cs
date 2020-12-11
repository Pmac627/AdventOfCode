namespace AdventOfCode.DTO
{
    public class RealPasswordRule
    {
        private const char _segmentDelimiter = ' ';
        private const char _minMaxRuleDelimiter = '-';

        public RealPasswordRule(string value)
        {
            var parts = value.Split(_segmentDelimiter);
            var sizeRules = parts[0].Split(_minMaxRuleDelimiter);
            var charRule = parts[1].Substring(0, 1);

            if (int.TryParse(sizeRules[0], out var index1))
            {
                Index1 = index1 - 1;
            }

            if (int.TryParse(sizeRules[1], out var index2))
            {
                Index2 = index2 - 1;
            }

            if (char.TryParse(charRule, out var character))
            {
                Character = character;
            }

            Password = parts[2].ToCharArray();
        }

        public int Index1 { get; init; }
        public int Index2 { get; init; }
        public char Character { get; init; }
        public char[] Password { get; init; }

        public bool IsValid()
        {
            var valid = false;
            var maxIndex = Password.Length - 1;

            if (Index1 <= maxIndex && Index2 <= maxIndex)
            {
                valid = (Character == Password[Index1] && Character != Password[Index2])
                    || (Character != Password[Index1] && Character == Password[Index2]);
            }

            return valid;
        }
    }
}
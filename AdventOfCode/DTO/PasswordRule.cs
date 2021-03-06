﻿namespace AdventOfCode.DTO
{
    public class PasswordRule
    {
        private const char _segmentDelimiter = ' ';
        private const char _minMaxRuleDelimiter = '-';

        public PasswordRule(string value)
        {
            var parts = value.Split(_segmentDelimiter);
            var sizeRules = parts[0].Split(_minMaxRuleDelimiter);
            var charRule = parts[1].Substring(0, 1);

            if (int.TryParse(sizeRules[0], out var minCount))
            {
                MinCount = minCount;
            }

            if (int.TryParse(sizeRules[1], out var maxCount))
            {
                MaxCount = maxCount;
            }

            if (char.TryParse(charRule, out var character))
            {
                Character = character;
            }

            Password = parts[2].ToCharArray();
        }

        public int MinCount { get; init; }
        public int MaxCount { get; init; }
        public char Character { get; init; }
        public char[] Password { get; init; }

        public bool IsValid()
        {
            var total = 0;

            for (var i = Password.Length - 1; i >= 0; i--)
            {
                if (Password[i] == Character)
                {
                    ++total;
                }
            }

            if (total >= MinCount && total <= MaxCount)
            {
                return true;
            }

            return false;
        }
    }
}
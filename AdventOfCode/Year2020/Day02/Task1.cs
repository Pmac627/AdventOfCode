using AdventOfCode.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day02
{
    public class Task1 : IRunnableCode
    {
        public async Task<string> ExecuteAsync(string[] data)
        {
            var totalValid = data.Count();

            for (var i = data.Count() - 1; i >= 0; i--)
            {
                if (!new PasswordRule(data[i]).IsValid())
                {
                    --totalValid;
                }
            }

            return totalValid.ToString();
        }

        private class PasswordRule
        {
            public PasswordRule(string value)
            {
                var parts = value.Split(' ');
                var sizeRules = parts[0].Split('-');
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

            public int MinCount { get; private set; }
            public int MaxCount { get; private set; }
            public char Character { get; private set; }
            public char[] Password { get; private set; }

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
}
using AdventOfCode.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day02
{
    public class Task2 : IRunnableCode
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

            public int Index1 { get; private set; }
            public int Index2 { get; private set; }
            public char Character { get; private set; }
            public char[] Password { get; private set; }

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
}
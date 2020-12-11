using AdventOfCode.DTO;
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day02
{
    [ExpectedResult("556")]
    public class Task1 : IRunnableCode
    {
        public async Task<string> ExecuteAsync(string[] data)
        {
            var totalValid = data.Length;

            for (var i = totalValid - 1; i >= 0; i--)
            {
                var passwordRule = new PasswordRule(data[i]);
                if (!passwordRule.IsValid())
                {
                    --totalValid;
                }
            }

            return totalValid.ToString();
        }
    }
}
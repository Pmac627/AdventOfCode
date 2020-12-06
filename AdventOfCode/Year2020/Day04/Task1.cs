using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day04
{
    public class Task1 : IRunnableCode
    {
        private static IList<string> _requiredCodes = new List<string>
        {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid"
        };

        public async Task<string> ExecuteAsync(string[] data)
        {
            var validCount = 0;
            var partCounts = 0;

            foreach (var line in data)
            {
                if (line == string.Empty)
                {
                    if (IsValid(partCounts))
                    {
                        validCount++;
                    }

                    partCounts = 0;

                    continue;
                }

                var splitParts = line.Split(' ');

                foreach (var code in _requiredCodes)
                {
                    partCounts += splitParts.Count(x => x.StartsWith(code));
                }
            }

            if (IsValid(partCounts))
            {
                validCount++;
            }

            return validCount.ToString();
        }

        private bool IsValid (int count)
        {
            return count >= _requiredCodes.Count;
        }
    }
}
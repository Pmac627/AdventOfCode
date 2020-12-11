using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day04
{
    [ExpectedResult("192")]
    public class Task1 : IRunnableCode
    {
        private const int _requiredValidParts = 7;

        private const char _codeDelimiter = ' ';

        private const string _codeBirthYear = "byr";
        private const string _codeIssueYear = "iyr";
        private const string _codeExpireYear = "eyr";
        private const string _codeHeight = "hgt";
        private const string _codeHairColor = "hcl";
        private const string _codeEyeColor = "ecl";
        private const string _codePassportId = "pid";

        private static readonly string[] _requiredCodes = new string[7] { _codeBirthYear, _codeIssueYear, _codeExpireYear, _codeHeight, _codeHairColor, _codeEyeColor, _codePassportId };

        public async Task<string> ExecuteAsync(string[] data)
        {
            var validCount = 0;
            var partCounts = 0;

            foreach (var line in data)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (IsValid(partCounts))
                    {
                        validCount++;
                    }

                    partCounts = 0;

                    continue;
                }

                partCounts += CountMatchingCodes(line.Split(_codeDelimiter));
            }

            if (IsValid(partCounts))
            {
                validCount++;
            }

            return validCount.ToString();
        }

        private static bool IsValid (int count)
        {
            return count >= _requiredValidParts;
        }

        private static int CountMatchingCodes(string[] codesProvided)
        {
            var matches = 0;

            foreach (var code in _requiredCodes)
            {
                matches += codesProvided.Count(x => x.StartsWith(code));
            }

            return matches;
        }
    }
}
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day04
{
    [ExpectedResult("101")]
    public class Task2 : IRunnableCode
    {
        private const int _requiredValidParts = 7;
        private const int _minBirthYear = 1920;
        private const int _maxBirthYear = 2002;
        private const int _minIssueYear = 2010;
        private const int _maxIssueYear = 2020;
        private const int _minExpireYear = 2020;
        private const int _maxExpireYear = 2030;
        private const int _minHeightCm = 150;
        private const int _maxHeightCm = 193;
        private const int _minHeightIn = 59;
        private const int _maxHeightIn = 76;
        private const int _hairColorCodeLength = 7;
        private const int _passportIdLength = 9;

        private const char _codeDelimiter = ' ';
        private const char _codeValueDelimiter = ':';

        private const string _codeBirthYear = "byr";
        private const string _codeIssueYear = "iyr";
        private const string _codeExpireYear = "eyr";
        private const string _codeHeight = "hgt";
        private const string _codeHairColor = "hcl";
        private const string _codeEyeColor = "ecl";
        private const string _codePassportId = "pid";
        private const string _heightCmSuffix = "cm";
        private const string _heightInSuffix = "in";
        
        private static readonly Regex _hairColorRegex = new Regex(@"^#(?:[0-9a-fA-F]{3}){1,2}$");
        private static readonly Regex _passpordIdRegex = new Regex(@"^[0-9]{9}$");
        private static readonly string[] _eyeColorOptions = new string[7] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

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

                var splitParts = line.Split(_codeDelimiter);

                foreach (var parts in splitParts)
                {
                    var x = parts.Split(_codeValueDelimiter);
                    var val = x[1].ToString();

                    switch (x[0].ToString())
                    {
                        case _codeBirthYear:

                            if (ValidateBirthYear(val))
                            {
                                partCounts++;
                            }

                            break;
                        case _codeIssueYear:

                            if (ValidateIssueYear(val))
                            {
                                partCounts++;
                            }

                            break;
                        case _codeExpireYear:

                            if (ValidateExpireYear(val))
                            {
                                partCounts++;
                            }

                            break;
                        case _codeHeight:

                            if (ValidateHeight(val))
                            {
                                partCounts++;
                            }

                            break;
                        case _codeHairColor:

                            if (ValidateHairColor(val))
                            {
                                partCounts++;
                            }

                            break;
                        case _codeEyeColor:

                            if (ValidateEyeColor(val))
                            {
                                partCounts++;
                            }

                            break;
                        case _codePassportId:

                            if (ValidatePassportId(val))
                            {
                                partCounts++;
                            }

                            break;
                        default:
                            break;
                    }
                }
            }

            if (IsValid(partCounts))
            {
                validCount++;
            }

            return validCount.ToString();
        }

        private static bool IsValid(int count)
        {
            return count >= _requiredValidParts;
        }

        private static bool ValidateBirthYear(string value)
        {
            if (int.TryParse(value, out var year))
            {
                return year >= _minBirthYear && year <= _maxBirthYear;
            }

            return false;
        }

        private static bool ValidateIssueYear(string value)
        {
            if (int.TryParse(value, out var year))
            {
                return year >= _minIssueYear && year <= _maxIssueYear;
            }

            return false;
        }

        private static bool ValidateExpireYear(string value)
        {
            if (int.TryParse(value, out var year))
            {
                return year >= _minExpireYear && year <= _maxExpireYear;
            }

            return false;
        }

        private static bool ValidateHeight(string value)
        {
            if (value.EndsWith(_heightCmSuffix))
            {
                var cmH = value.Remove(value.Length - 2, 2);

                if (int.TryParse(cmH, out var cm))
                {
                    return cm >= _minHeightCm && cm <= _maxHeightCm;
                }

                return false;
            }
            else if (value.EndsWith(_heightInSuffix))
            {
                var inH = value.Remove(value.Length - 2, 2);

                if (int.TryParse(inH, out var inch))
                {
                    return inch >= _minHeightIn && inch <= _maxHeightIn;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        private static bool ValidateHairColor(string value)
        {
            if (value.Length == _hairColorCodeLength)
            {
                return _hairColorRegex.IsMatch(value);
            }

            return false;
        }

        private static bool ValidateEyeColor(string value)
        {
            if (_eyeColorOptions.Contains(value))
            {
                return true;
            }

            return false;
        }

        private static bool ValidatePassportId(string value)
        {
            if (value.Length == _passportIdLength)
            {
                return _passpordIdRegex.IsMatch(value);
            }

            return false;
        }
    }
}
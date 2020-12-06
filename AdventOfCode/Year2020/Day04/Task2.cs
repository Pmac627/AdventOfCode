using AdventOfCode.Interfaces;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day04
{
    public class Task2 : IRunnableCode
    {
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

                foreach (var parts in splitParts)
                {
                    var x = parts.Split(':');
                    var val = x[1].ToString();

                    switch (x[0].ToString())
                    {
                        case "byr":

                            if (ValidateBirthYear(val))
                            {
                                partCounts++;
                            }

                            break;
                        case "iyr":

                            if (ValidateIssueYear(val))
                            {
                                partCounts++;
                            }

                            break;
                        case "eyr":

                            if (ValidateExpireYear(val))
                            {
                                partCounts++;
                            }

                            break;
                        case "hgt":

                            if (ValidateHeight(val))
                            {
                                partCounts++;
                            }

                            break;
                        case "hcl":

                            if (ValidateHairColor(val))
                            {
                                partCounts++;
                            }

                            break;
                        case "ecl":

                            if (ValidateEyeColor(val))
                            {
                                partCounts++;
                            }

                            break;
                        case "pid":

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

        private bool IsValid(int count)
        {
            return count >= 7;
        }

        private bool ValidateBirthYear(string value)
        {
            if (int.TryParse(value, out var year))
            {
                return year >= 1920 && year <= 2002;
            }

            return false;
        }

        private bool ValidateIssueYear(string value)
        {
            if (int.TryParse(value, out var year))
            {
                return year >= 2010 && year <= 2020;
            }

            return false;
        }

        private bool ValidateExpireYear(string value)
        {
            if (int.TryParse(value, out var year))
            {
                return year >= 2020 && year <= 2030;
            }

            return false;
        }

        private bool ValidateHeight(string value)
        {
            if (value.EndsWith("cm"))
            {
                var cmH = value.Remove(value.Length - 2, 2);

                if (int.TryParse(cmH, out var cm))
                {
                    return cm >= 150 && cm <= 193;
                }

                return false;
            }
            else if (value.EndsWith("in"))
            {
                var inH = value.Remove(value.Length - 2, 2);

                if (int.TryParse(inH, out var inch))
                {
                    return inch >= 59 && inch <= 76;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateHairColor(string value)
        {
            if (value.Length == 7)
            {
                var rgx = new Regex(@"^#(?:[0-9a-fA-F]{3}){1,2}$");

                return rgx.IsMatch(value);
            }

            return false;
        }

        private bool ValidateEyeColor(string value)
        {
            switch (value)
            {
                case "amb":
                case "blu":
                case "brn":
                case "gry":
                case "grn":
                case "hzl":
                case "oth":
                    return true;
                default:
                    return false;
            }
        }

        private bool ValidatePassportId(string value)
        {
            if (value.Length == 9)
            {
                var rgx = new Regex(@"^[0-9]{9}$");

                return rgx.IsMatch(value);
            }

            return false;
        }
    }
}
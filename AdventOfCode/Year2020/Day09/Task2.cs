using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day09
{
    [ExpectedResult("5388976")]
    public class Task2 : IRunnableCode
    {
        private const long _targetValue = 41682220L;

        public async Task<string> ExecuteAsync(string[] data)
        {
            var numbers = data.Select(x => long.Parse(x)).ToList();

            var contiguousNumbers = new List<long>();

            for (var i = 0; i < numbers.Count; i++)
            {
                var num = numbers[i];

                if (i > 1 && contiguousNumbers.Sum() == _targetValue)
                {
                    return GetFinalValue(contiguousNumbers);
                }

                contiguousNumbers.Add(num);

                if (contiguousNumbers.Sum() == _targetValue)
                {
                    break;
                }

                while (contiguousNumbers.Sum() > _targetValue)
                {
                    contiguousNumbers.RemoveAt(0);

                    if (contiguousNumbers.Sum() == _targetValue)
                    {
                        break;
                    }
                }
            }

            return GetFinalValue(contiguousNumbers);
        }

        private static string GetFinalValue(List<long> numbers)
        {
            var finalValue = numbers.Min() + numbers.Max();
            return finalValue.ToString();
        }
    }
}
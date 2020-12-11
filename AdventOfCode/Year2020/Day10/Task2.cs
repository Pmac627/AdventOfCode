using AdventOfCode.DataManagement;
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AdventOfCode.DTO.Enums;

namespace AdventOfCode.Year2020.Day10
{
    [ExpectedResult("3100448333024")]
    public class Task2 : IRunnableCode
    {
        private const int _defaultJolt = 0;
        private const int _oneJolt = 1;
        private const int _threeJolt = 3;

        public async Task<string> ExecuteAsync(string[] data)
        {
            var numbers = data.ToNumberList<long>();

            var result = SolvePuzzle(numbers);

            return result.ToString();
        }

        public static long SolvePuzzle(List<long> numbers)
        {
            var highestJolt = numbers.Max() + _threeJolt;

            numbers.Add(_defaultJolt);
            numbers.Add(highestJolt);

            numbers = numbers.Sort(SortRule.DefaultComparer);

            var numberCount = numbers.Count;

            var steps = new long[numberCount];

            steps[numberCount - 1] = _oneJolt;

            for (var i = numberCount - 1; i > _defaultJolt; i--)
            {
                for (var j = 1; j <= _threeJolt; j++)
                {
                    var pos = numbers.IndexOf(numbers[i] - j);

                    if (pos >= _defaultJolt)
                    {
                        steps[pos] += steps[i];
                    }
                }
            }

            return steps.First();
        }
    }
}
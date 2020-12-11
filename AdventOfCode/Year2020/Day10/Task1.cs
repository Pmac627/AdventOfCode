using AdventOfCode.DataManagement;
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day10
{
    [ExpectedResult("1625")]
    public class Task1 : IRunnableCode
    {
        private const int _defaultJolt = 0;
        private const int _oneJoltOver = 1;
        private const int _twoJoltOver = 2;
        private const int _threeJoltOver = 3;

        public async Task<string> ExecuteAsync(string[] data)
        {
            var numbers = data.ToNumberList<int>();

            var result = SolvePuzzle(numbers);

            return result.ToString();
        }

        public static int SolvePuzzle(IList<int> numbers)
        {
            var highestJolt = numbers.Max() + _threeJoltOver;

            numbers.Add(_defaultJolt);
            numbers.Add(highestJolt);

            var oneJoltCount = numbers
                .Count(x => numbers.Contains(x + _oneJoltOver));

            var threeJoltCount = numbers
                .Count(x => !numbers.Contains(x + _oneJoltOver)
                    && !numbers.Contains(x + _twoJoltOver)
                    && numbers.Contains(x + _threeJoltOver));

            return Multiply(oneJoltCount, threeJoltCount);
        }

        private static int Multiply(int first, int second)
        {
            return first * second;
        }
    }
}
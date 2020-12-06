using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day01
{
    [ExpectedResult("866436")]
    public class Task1 : IRunnableCode
    {
        private const int _targetTotal = 2020;

        public async Task<string> ExecuteAsync(string[] data)
        {
            try
            {
                var numbers = data.Select(x => int.Parse(x)).ToList();

                for (var i = numbers.Count - 1; i >= 0; i--)
                {
                    var diff = _targetTotal - numbers[i];

                    if (numbers.Contains(diff))
                    {
                        return Multiply(numbers[i], diff).ToString();
                    }
                }

                return "Failed!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static long Multiply(int first, int second)
        {
            return first * second;
        }
    }
}
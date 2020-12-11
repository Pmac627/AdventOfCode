using AdventOfCode.DataManagement;
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day01
{
    [ExpectedResult("276650720")]
    public class Task2 : IRunnableCode
    {
        private const int _targetTotal = 2020;

        public async Task<string> ExecuteAsync(string[] data)
        {
            try
            {
                var numbers = data.ToNumberList<int>();

                for (var i = numbers.Count - 1; i >= 0; i--)
                {
                    var diff = _targetTotal - numbers[i];

                    for (var j = numbers.Count - 1; j >= 0; j--)
                    {
                        if (numbers[j] != numbers[i])
                        {
                            var balance = diff - numbers[j];

                            if (numbers.Contains(balance))
                            {
                                return Multiply(numbers[i], numbers[j], balance).ToString();
                            }
                        }
                    }
                }

                return "Failed!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static long Multiply(int first, int second, int third)
        {
            return first * second * third;
        }
    }
}
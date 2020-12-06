using AdventOfCode.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day01
{
    public class Task2 : IRunnableCode
    {
        public async Task<string> ExecuteAsync(string[] data)
        {
            try
            {
                var goal = 2020;
                var numbers = data.Select(x => int.Parse(x)).ToList();

                for (var i = numbers.Count - 1; i >= 0; i--)
                {
                    var diff = goal - numbers[i];

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

        private long Multiply(int first, int second, int third)
        {
            return first * second * third;
        }
    }
}
using AdventOfCode.DataManagement;
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day09
{
    [ExpectedResult("41682220")]
    public class Task1 : IRunnableCode
    {
        private static readonly (int Index, int Length) _preambleRange = new (0, 25);

        public async Task<string> ExecuteAsync(string[] data)
        {
            var numbers = data.ToNumberList<long>();

            var preamble = numbers.GetRange(_preambleRange.Index, _preambleRange.Length);
            var remaining = numbers.GetRange(_preambleRange.Length, numbers.Count - _preambleRange.Length);

            foreach (var num in remaining)
            {
                if (!FindOtherNumber(preamble, num))
                {
                    return num.ToString();
                }

                preamble.RemoveAt(0);
                preamble.Add(num);
            }

            return "Failed!";
        }

        public static bool FindOtherNumber(IList<long> preamble, long number)
        {
            foreach (var pre in preamble)
            {
                if (preamble.Contains(number - pre))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
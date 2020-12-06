using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day06
{
	[ExpectedResult("6612")]
    public class Task1 : IRunnableCode
    {
        public async Task<string> ExecuteAsync(string[] data)
        {
            var answerTotal = 0;

            HashSet<char> uniqueAnswers = new HashSet<char>();

            foreach (var line in data)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    answerTotal += uniqueAnswers.Count;

                    uniqueAnswers.Clear();

                    continue;
                }

                PopulateUniqueAnswers(line.Distinct(), ref uniqueAnswers);
            }

            answerTotal += uniqueAnswers.Count;

            return answerTotal.ToString();
        }

        private void PopulateUniqueAnswers(IEnumerable<char> chars, ref HashSet<char> uniqueAnswers)
        {
            foreach (var c in chars)
            {
                if (!uniqueAnswers.Contains(c))
                {
                    uniqueAnswers.Add(c);
                }
            }
        }
    }
}
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day06
{
    [ExpectedResult("3268")]
    public class Task2 : IRunnableCode
    {
        public async Task<string> ExecuteAsync(string[] data)
        {
            var groupAnswers = new Dictionary<char, int>();
            var totalMembers = 0;
            var answerTotal = 0;

            foreach (var line in data)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    answerTotal += GetAllSameAnswerCount(totalMembers, groupAnswers.Values.ToList());

                    totalMembers = 0;
                    groupAnswers.Clear();

                    continue;
                }

                ++totalMembers;

                PopulateGroupAnswers(line.Distinct(), ref groupAnswers);
            }

            answerTotal += GetAllSameAnswerCount(totalMembers, groupAnswers.Values.ToList());

            return answerTotal.ToString();
        }

        private static int GetAllSameAnswerCount(int totalMembers, IList<int> answers)
        {
            var answerTotal = 0;

            foreach (var answer in answers)
            {
                if (answer == totalMembers)
                {
                    answerTotal++;
                }
            }

            return answerTotal;
        }

        private static void PopulateGroupAnswers(IEnumerable<char> chars, ref Dictionary<char, int> groupAnswers)
        {
            foreach (var c in chars)
            {
                if (!groupAnswers.ContainsKey(c))
                {
                    groupAnswers.Add(c, 1);
                }
                else
                {
                    groupAnswers[c]++;
                }
            }
        }
    }
}
using AdventOfCode.DTO;
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day07
{
    [ExpectedResult("10219")]
    public class Task2 : IRunnableCode
    {
        private const string _targetBag = "shiny gold";
        private const string _bagToRuleDelimiter = " bags contain ";
        private const string _emptyBagPhrase = "no other bags.";
        private const string _containedBagDelimiter = ", ";
        private const string _bagSuffix = " bag";
        private const string _bagsSuffix = " bags";

        private const char _bagRuleDelimiter = ' ';

        public async Task<string> ExecuteAsync(string[] data)
        {
            var bagRules = new List<BagRule>();

            foreach (var rules in data)
            {
                var ruleParts = rules.Split(_bagToRuleDelimiter, StringSplitOptions.RemoveEmptyEntries);
                var bagRule = new BagRule(ruleParts[0]);

                if ((ruleParts[1] != _emptyBagPhrase))
                {
                    var containedBagsParts = ruleParts[1].Remove(ruleParts[1].Length - 1, 1).Split(_containedBagDelimiter, StringSplitOptions.RemoveEmptyEntries);

                    var containedBags = containedBagsParts
                        .Select(x => x.Replace(_bagsSuffix, string.Empty).Replace(_bagSuffix, string.Empty).Split(_bagRuleDelimiter, StringSplitOptions.RemoveEmptyEntries))
                        .Select(x => new BagContents(int.Parse(x[0]), string.Join(_bagRuleDelimiter, x.Skip(1))))
                        .ToList();

                    bagRule.AddContents(containedBags);

                    bagRules.Add(bagRule);
                }
                else
                {
                    bagRules.Add(bagRule);
                }
            }

            return CountBags(bagRules).ToString();
        }

        private static int CountBags(List<BagRule> bagRules, string targetBag = _targetBag)
        {
            var totalBags = 0;
            var bagRule = bagRules.FirstOrDefault(x => x.Bag == targetBag);

            if (!bagRule.IsEmpty)
            {
                foreach (var content in bagRule.Contents)
                {
                    totalBags += content.Count + content.Count * CountBags(bagRules, content.Bag);
                }
            }

            return totalBags;
        }
    }
}
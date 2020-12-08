using AdventOfCode.DTO;
using AdventOfCode.DTO.Attributes;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Day07
{
    [ExpectedResult("142")]
    public class Task1 : IRunnableCode
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
            var totalBagsNeeded = 0;
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

            var bagsToReview = new List<BagRule>
            {
                bagRules.FirstOrDefault(x => x.Bag == _targetBag)
            };

            var allValidBags = new HashSet<string>();

            BagsThatCouldContainTargetBag(bagRules, new List<string> { _targetBag }, ref allValidBags);

            totalBagsNeeded = allValidBags.Count;

            return totalBagsNeeded.ToString();
        }

        public void BagsThatCouldContainTargetBag(List<BagRule> bagRules, IList<string> bags, ref HashSet<string> allValidBags)
        {
            var bagsThatCouldContainTargets = new List<string>();

            foreach (var bag in bags)
            {
                bagsThatCouldContainTargets.AddRange(bagRules.Where(x => !x.IsEmpty && x.Contents.Any(y => y.Bag.Equals(bag, StringComparison.OrdinalIgnoreCase))).Select(x => x.Bag));
            }

            if (bagsThatCouldContainTargets != null && bagsThatCouldContainTargets.Any())
            {
                foreach (var bag in bagsThatCouldContainTargets)
                {
                    if (!allValidBags.Contains(bag))
                    {
                        allValidBags.Add(bag);
                    }
                }

                BagsThatCouldContainTargetBag(bagRules, bagsThatCouldContainTargets, ref allValidBags);
            }
        }
    }
}
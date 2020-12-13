using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc.day7
{
    public class Rule
    {
        public readonly string Container;
        public readonly (int Count, string Color)[] Items;

        private static readonly Regex ContainerRegex = new Regex(@"^(\w+ \w+)", RegexOptions.Compiled);
        private static readonly Regex ItemRegex = new Regex(@"(\d+) (\w+ \w+)", RegexOptions.Compiled);
        public Rule(string input)
        {
            Container = ContainerRegex.Match(input).Groups[1].Value;
            Items = ItemRegex
                .Matches(input)
                .Select(match => (
                    int.Parse(match.Groups[1].Value),
                    match.Groups[2].Value))
                .ToArray();
        }

        public int CountFor(string color) => Items.Append((0, color)).First(i => i.Item2 == color).Item1;

        public override string ToString() =>
            $"{Container} contain {string.Join(", ", Items.Select(i => $"{i.Count} {i.Color}"))}.";
    }

    public class RuleSet
    {
        public readonly IReadOnlyDictionary<string, Rule> Rules;
        public readonly IReadOnlyDictionary<string, IReadOnlyList<Rule>> RulesByItem;

        public RuleSet(string input) : this(input.Split('\n')) { }
        public RuleSet(IEnumerable<string> lines)
        {
            Rules = lines
                .Select(l => l.Trim())
                .Where(l => l.Length > 0)
                .Select(l => new Rule(l))
                .ToDictionary(r => r.Container, r => r);

            RulesByItem = Rules.Values
                .SelectMany(rule => rule.Items.Select(item => (item.Color, rule)))
                .GroupBy(ruleByItem => ruleByItem.Color)
                .ToDictionary(group => group.Key, group => group.Select(i => i.rule).ToArray() as IReadOnlyList<Rule>);
        }

        public IEnumerable<IReadOnlyList<Rule>> RuleChainsThatContain(string color)
        {
            IEnumerable<IEnumerable<Rule>> SubRuleChainsThatContain(string color, IEnumerable<Rule> childChain)
            {
                if (!RulesByItem.TryGetValue(color, out var myList))
                    return Enumerable.Empty<IEnumerable<Rule>>();
                return myList.SelectMany(i =>
                    new[] { childChain.Prepend(i) }
                    .Concat(SubRuleChainsThatContain(i.Container, childChain.Prepend(i))));
            }

            return SubRuleChainsThatContain(color, Enumerable.Empty<Rule>())
                .Select(chain => chain.ToArray() as IReadOnlyList<Rule>)
                .Distinct(new RuleChainEqualityComparer());
        }

        public IEnumerable<string> ColorsThatCanProduce(string color) => RuleChainsThatContain(color)
            .Select(chain => chain.First().Container)
            .Distinct();

        public int BagCountInside(string color)
        {
            int? SubBagCountInside(string color, IEnumerable<string> parents)
            {
                if (parents.Contains(color))
                    return null;

                var rule = Rules[color];
                int count = 1;
                foreach (var item in rule.Items)
                {
                    var addCount = SubBagCountInside(item.Color, parents.Append(color));
                    if (!addCount.HasValue)
                        return null;
                    count += addCount.Value * item.Count;
                }
                return count;
            }

            return (SubBagCountInside("shiny gold", Enumerable.Empty<string>()) ?? throw new InvalidProgramException("RECURSION")) - 1;
        }
    }

    public class RuleChainEqualityComparer : IEqualityComparer<IReadOnlyList<Rule>>
    {
        public bool Equals(IReadOnlyList<Rule>? x, IReadOnlyList<Rule>? y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return ReferenceEquals(x, y);
            return x.SequenceEqual(y);
        }

        public int GetHashCode(IReadOnlyList<Rule> obj)
        {
            unchecked
            {
                int hash = (int)2166136261;
                foreach (var rule in obj)
                    hash = (hash ^ rule.GetHashCode()) * 16777619;
                return hash;
            }
        }
    }

    public static class Day7
    {
        public static void Run()
        {
            var inputRuleSet = new RuleSet(File.ReadAllLines("day7/input.txt"));

            Console.WriteLine(inputRuleSet.ColorsThatCanProduce("shiny gold").Count());
            Console.WriteLine(inputRuleSet.BagCountInside("shiny gold"));
        }
    }
}

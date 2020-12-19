using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace aoc.day19
{
    public struct Rule
    {
        public int Producing;
        public string? Literal;
        public int[][]? Rules;

        private static readonly Regex RuleRegex = new Regex(@"(\d+): ([0-9 ]+)(\|[0-9 ]+)?", RegexOptions.Compiled);
        private static readonly Regex LiteralRegex = new Regex(@"(\d+): \""(\w+)\""", RegexOptions.Compiled);
        public Rule(string line)
        {
            int[] ParseRuleGroup(string v) => v.Replace("|", "").Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();

            var ruleMatch = RuleRegex.Match(line);
            var literalMatch = LiteralRegex.Match(line);
            if (literalMatch.Success)
            {
                Producing = int.Parse(literalMatch.Groups[1].Value);
                Literal = literalMatch.Groups[2].Value;
                Rules = null;
            }
            else if (ruleMatch.Success)
            {
                Producing = int.Parse(ruleMatch.Groups[1].Value);
                Literal = null;
                if (ruleMatch.Groups[3].Success)
                    Rules = new int[][] { ParseRuleGroup(ruleMatch.Groups[2].Value), ParseRuleGroup(ruleMatch.Groups[3].Value) };
                else
                    Rules = new int[][] { ParseRuleGroup(ruleMatch.Groups[2].Value) };
            }
            else
                throw new InvalidDataException();
        }
    }

    public static class Day19
    {
        public static string ToRegexSource(Rule[] rules, bool fixSome = false)
        {
            var rulesById = rules.ToDictionary(r => r.Producing, r => r);
            return "^" + ToRegexSourceInner2(rulesById[0]) + "$";

            string ToRegexSourceInner(Rule rule)
            {
                if (rule.Literal != null)
                    return "(" + rule.Literal + ")";
                if (rule.Rules!.Length == 1)
                    return "(" + string.Join("", rule.Rules[0].Select(i => ToRegexSourceInner2(rulesById[i]))) + ")";
                return "((" + string.Join(")|(", rule.Rules.Select(r => string.Join("", r.Select(i => ToRegexSourceInner2(rulesById[i]))))) + "))";
            }

            string ToRegexSourceInner2(Rule rule)
            {
                if (!fixSome || (rule.Producing != 8 && rule.Producing != 11))
                    return ToRegexSourceInner(rule);

                var r42 = ToRegexSourceInner(rulesById[42]);
                var r31 = ToRegexSourceInner(rulesById[31]);
                if (rule.Producing == 8)
                    return $"({r42}+)";
                if (rule.Producing == 11)
                    return $"((?<N>{r42})+(?<-N>{r31})+(?(N)(?!)))";
                throw new InvalidDataException();
            }
        }

        public static Regex ToRegex(Rule[] rules, bool fixSome = false) => new Regex(ToRegexSource(rules, fixSome), RegexOptions.Compiled);

        public static void Run()
        {
            var inputRules = File.ReadAllLines("day19/rules.txt").Select(l => new Rule(l)).ToArray();
            //var inputRegex = ToRegex(inputRules);
            var inputRegex2 = ToRegex(inputRules, true);
            var inputPhrases = File.ReadAllLines("day19/phrases.txt");

            //Console.WriteLine(inputPhrases.Count(p => inputRegex.IsMatch(p)));
            Console.WriteLine(inputPhrases.Count(p => inputRegex2.IsMatch(p)));
        }
    }
}

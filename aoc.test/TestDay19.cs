using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day19;
using NUnit.Framework;
using static aoc.day19.Day19;

namespace aoc.test
{
    class TestDay19
    {
        private static readonly string[] ExampleRuleLines = new[]
        {
            "0: 4 1 5",
            "1: 2 3 | 3 2",
            "2: 4 4 | 5 5",
            "3: 4 5 | 5 4",
            "4: \"a\"",
            "5: \"b\""
        };

        [Test]
        public void TestRightExamples()
        {
            var exampleRules = ExampleRuleLines.Select(l => new Rule(l)).ToArray();
            var regex = ToRegex(exampleRules);

            Assert.True(regex.IsMatch("aaaabb"));
            Assert.True(regex.IsMatch("aaabab"));
            Assert.True(regex.IsMatch("abbabb"));
            Assert.True(regex.IsMatch("abbbab"));
            Assert.True(regex.IsMatch("aabaab"));
            Assert.True(regex.IsMatch("aabbbb"));
            Assert.True(regex.IsMatch("abaaab"));
            Assert.True(regex.IsMatch("ababbb"));
        }

        [Test]
        public void TestWrongExamples()
        {
            var exampleRules = ExampleRuleLines.Select(l => new Rule(l)).ToArray();
            var regex = ToRegex(exampleRules);

            Assert.False(regex.IsMatch("abaabb"));
            Assert.False(regex.IsMatch("aaaaaa"));
            Assert.False(regex.IsMatch("bbbbbb"));
            Assert.False(regex.IsMatch("abbbbb"));
            Assert.False(regex.IsMatch("aabbab"));
        }

        private static readonly string[] ExampleRuleLines2 = new[]
        {
            "42: 9 14 | 10 1",
            "9: 14 27 | 1 26",
            "10: 23 14 | 28 1",
            "1: \"a\"",
            "11: 42 31",
            "5: 1 14 | 15 1",
            "19: 14 1 | 14 14",
            "12: 24 14 | 19 1",
            "16: 15 1 | 14 14",
            "31: 14 17 | 1 13",
            "6: 14 14 | 1 14",
            "2: 1 24 | 14 4",
            "0: 8 11",
            "13: 14 3 | 1 12",
            "15: 1 | 14",
            "17: 14 2 | 1 7",
            "23: 25 1 | 22 14",
            "28: 16 1",
            "4: 1 1",
            "20: 14 14 | 1 15",
            "3: 5 14 | 16 1",
            "27: 1 6 | 14 18",
            "14: \"b\"",
            "21: 14 1 | 1 14",
            "25: 1 1 | 1 14",
            "22: 14 14",
            "8: 42",
            "26: 14 22 | 1 20",
            "18: 15 15",
            "7: 14 5 | 1 21",
            "24: 14 1",
        };

        public static readonly string[] ExampleWrongPhrases = new[]
        {
            "abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa",
            "babbbbaabbbbbabbbbbbaabaaabaaa",
            "aaabbbbbbaaaabaababaabababbabaaabbababababaaa",
            "bbbbbbbaaaabbbbaaabbabaaa",
            "bbbababbbbaaaaaaaabbababaaababaabab",
            "baabbaaaabbaaaababbaababb",
            "abbbbabbbbaaaababbbbbbaaaababb",
            "aaaaabbaabaaaaababaa",
            "aaaabbaaaabbaaa",
            "aaaabbaabbaaaaaaabbbabbbaaabbaabaaa",
            "babaaabbbaaabaababbaabababaaab",
            "aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba",
        };

        public static readonly string[] ExampleRightPhrases = new[]
        {
            "bbabbbbaabaabba",
            "ababaaaaaabaaab",
            "ababaaaaabbbaba"
        };

        [TestCaseSource(nameof(ExampleWrongPhrases))]
        public void TestWrongPhrase2(string phrase)
        {
            var regex = ToRegex(ExampleRuleLines2.Select(l => new Rule(l)).ToArray(), true);
            Assert.False(regex.IsMatch(phrase));
        }

        [TestCaseSource(nameof(ExampleRightPhrases))]
        public void TestRightPhrase2(string phrase)
        {
            var regex = ToRegex(ExampleRuleLines2.Select(l => new Rule(l)).ToArray(), true);
            Assert.True(regex.IsMatch(phrase));
        }

        [Test]
        public void TestBracketMatching()
        {
            var regex = ToRegex(new[]
            {
                "0: 11",
                "11: 42 31 | 42 11 31",
                "42: \"a\"",
                "31: \"b\""
            }.Select(l => new Rule(l)).ToArray(), true);

            Assert.True(regex.IsMatch("ab"));
            Assert.True(regex.IsMatch("aabb"));
            Assert.True(regex.IsMatch("aaabbb"));

            Assert.False(regex.IsMatch(""));
            Assert.False(regex.IsMatch("abb"));
            Assert.False(regex.IsMatch("aab"));
            Assert.False(regex.IsMatch("aaaaabbbb"));
        }

        [Test]
        public void TestMultiples()
        {
            var regex = ToRegex(new[]
            {
                "0: 8",
                "8: 42 | 42 8",
                "42: \"a\"",
                "31: \"b\""
            }.Select(l => new Rule(l)).ToArray(), true);

            Assert.True(regex.IsMatch("a"));
            Assert.True(regex.IsMatch("aa"));
            Assert.True(regex.IsMatch("aaaaaaaaaa"));

            Assert.False(regex.IsMatch(""));
            Assert.False(regex.IsMatch("b"));
            Assert.False(regex.IsMatch("aaaaaaab"));
            Assert.False(regex.IsMatch("baaaaa"));
        }
    }
}

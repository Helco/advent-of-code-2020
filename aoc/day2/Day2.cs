using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc.day2
{
    public class PasswordAndPolicy
    {
        public int minOccur, maxOccur;
        public char occurChar;
        public string password = "";

        public PasswordAndPolicy() { }
        public PasswordAndPolicy(int minOccur, int maxOccur, char occurChar, string password)
        {
            this.minOccur = minOccur;
            this.maxOccur = maxOccur;
            this.occurChar = occurChar;
            this.password = password;
        }

        public int OccurCharCount => password.Count(ch => ch == occurChar);
        public bool IsValid => OccurCharCount >= minOccur && OccurCharCount <= maxOccur;

        public bool IsValidNew => (password[minOccur - 1] == occurChar) ^ (password[maxOccur - 1] == occurChar);
    }

    public static class Day2
    {
        private static readonly Regex lineRegex = new Regex(@"^(\d+)-(\d+) (\w): (\w+)\s*$", RegexOptions.Compiled);
        public static PasswordAndPolicy SplitInputLine(string line)
        {
            var match = lineRegex.Match(line);
            if (!match.Success)
                throw new ArgumentException();

            return new PasswordAndPolicy()
            {
                minOccur = int.Parse(match.Groups[1].Value),
                maxOccur = int.Parse(match.Groups[2].Value),
                occurChar = match.Groups[3].Value.First(),
                password = match.Groups[4].Value,
            };
        }

        public static void Run()
        {
            Console.WriteLine(File.ReadAllLines("day2/input.txt")
                .Select(line => SplitInputLine(line))
                .Count(pap => pap.IsValid));

            Console.WriteLine(File.ReadAllLines("day2/input.txt")
                .Select(line => SplitInputLine(line))
                .Count(pap => pap.IsValidNew));
        }
    }
}

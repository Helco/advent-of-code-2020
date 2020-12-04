using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc.day4
{
    public class Passport
    {
        public IReadOnlyDictionary<string, string> fields;

        public Passport(IEnumerable<string> lines)
        {
            var fields = new Dictionary<string, string>();
            var parts = lines.SelectMany(l => l.Split(' ')).Select(p => p.Trim()).Where(p => p != "");
            foreach (var part in parts)
            {
                var subParts = part.Split(':');
                fields.Add(subParts[0], subParts[1]);
            }
            this.fields = fields;
        }

        // without cid
        private static readonly IReadOnlySet<string> FieldNames = new HashSet<string>()
        {
            "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid"
        };
        public bool IsValid => FieldNames.Except(new[] { "cid" }).All(fieldName => fields.ContainsKey(fieldName));

        private static bool ValidateNumber(string txt, int min, int max) =>
            int.TryParse(txt, out int value) && value >= min && value <= max;

        private static readonly Regex HeightRegex = new Regex(@"^(\d+)(cm|in)$", RegexOptions.Compiled);
        private static bool ValidateHeight(string txt)
        {
            var match = HeightRegex.Match(txt);
            if (!match.Success)
                return false;
            int value = int.Parse(match.Groups[1].Value);
            return match.Groups[2].Value switch
            {
                "cm" when value >= 150 && value <= 193  => true,
                "in" when value >= 59 && value <= 76    => true,

                _ => false
            };
        }

        private static readonly Regex HairColorRegex = new Regex(@"^#[0-9a-f]{6}$", RegexOptions.Compiled);
        private static bool ValidateHairColor(string txt) => HairColorRegex.IsMatch(txt);

        private static readonly IReadOnlySet<string> EyeColors = new HashSet<string>()
        {
            "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
        };
        private static bool ValidateEyeColor(string txt) => EyeColors.Contains(txt);

        private static readonly Regex PassportIDRegex = new Regex(@"^\d{9}$", RegexOptions.Compiled);
        private static bool ValidatePassportID(string txt) => PassportIDRegex.IsMatch(txt);

        public bool IsValidThorough => IsValid &&
            ValidateNumber(fields["byr"], 1920, 2002) &&
            ValidateNumber(fields["iyr"], 2010, 2020) &&
            ValidateNumber(fields["eyr"], 2020, 2030) &&
            ValidateHeight(fields["hgt"]) &&
            ValidateHairColor(fields["hcl"]) &&
            ValidateEyeColor(fields["ecl"]) &&
            ValidatePassportID(fields["pid"]);
    }

    public static class Day4
    {
        public static IEnumerable<Passport> ParsePassports(string input)
        {
            var lines = input.Split("\n");
            var curPassportContent = new List<string>();
            foreach (var line in lines)
            {
                if (line.Trim().Length == 0)
                {
                    if (curPassportContent.Count > 0)
                        yield return new Passport(curPassportContent);
                    curPassportContent = new List<string>();
                    continue;
                }
                curPassportContent.Add(line);
            }
            if (curPassportContent.Count > 0)
                yield return new Passport(curPassportContent);
        }

        public static void Run()
        {
            var passports = ParsePassports(File.ReadAllText("day4/input.txt")).ToArray();
            Console.WriteLine(passports.Count(p => p.IsValid));
            Console.WriteLine(passports.Count(p => p.IsValidThorough));
        }
    }
}

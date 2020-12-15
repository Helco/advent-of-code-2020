using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Numerics;

namespace aoc.day14.p2
{
    public readonly struct AddressPattern
    {
        public readonly long FloatingMask;
        public readonly long FixedValues;
        public readonly long AffectedCount => 1 << AoCMath.Count1Bits(FloatingMask);

        public AddressPattern(string line)
        {
            line = line.Trim();
            FloatingMask = line.Select(c => c == 'X' ? 1L : 0L).Aggregate((prev, cur) => prev << 1 | cur);
            FixedValues = line.Select(c => c == '1' ? 1L : 0L).Aggregate((prev, cur) => prev << 1 | cur);
        }

        public AddressPattern(long floatingMask, long fixedValues) => (FloatingMask, FixedValues) = (floatingMask, fixedValues);

        public AddressPattern Apply(long address) => new AddressPattern(FloatingMask, (address | FixedValues) & ~FloatingMask);

        public static AddressPattern? operator -(AddressPattern a, string bTxt) => a - new AddressPattern(bTxt);
        public static AddressPattern? operator -(AddressPattern? a, string bTxt) => a == null ? null : a.Value - new AddressPattern(bTxt);
        public static AddressPattern? operator -(AddressPattern? a, AddressPattern b) => a == null ? null : a.Value - b;
        public static AddressPattern? operator - (AddressPattern a, AddressPattern b)
        {
            // if fixed values are not equal, b does not affect a
            var combinedFixed = ~(a.FloatingMask | b.FloatingMask);
            if ((a.FixedValues & combinedFixed) != (b.FixedValues & combinedFixed))
                return a;

            // reduce floating in a with fixed of b
            var aFloatingButNotB = a.FloatingMask & ~b.FloatingMask;
            var newFixedValues = (a.FixedValues & ~aFloatingButNotB) | (~b.FixedValues & aFloatingButNotB);
            var newFloatingMask = a.FloatingMask ^ aFloatingButNotB;

            // if every fixed b is equal to a, b overrides a completly now
            if ((newFixedValues & ~b.FloatingMask) == (b.FixedValues & ~b.FloatingMask))
                return null;

            return new AddressPattern(newFloatingMask, newFixedValues);
        }

        public override string ToString()
        {
            int length = Math.Max(AoCMath.Log2(FloatingMask), AoCMath.Log2(FixedValues)) + 1;
            if (length <= 0)
                return "";

            var result = Enumerable.Repeat('-', length).ToArray();
            for (int i = 0; i < length; i++)
            {
                result[^(i + 1)] = (FloatingMask & (1L << i)) > 0 ? 'X' : (FixedValues & (1L << i)) > 0 ? '1' : '0';
            }
            return new string(result);
        }


    }
    public readonly struct WriteValueInstr
    {
        public readonly AddressPattern AddressPattern;
        public readonly long Value;

        public WriteValueInstr(AddressPattern address, long value) => (AddressPattern, Value) = (address, value);
    }

    public static class Day14
    {

        private static readonly Regex NewMaskRegex = new Regex(@"mask = ([X10]{36})", RegexOptions.Compiled);
        private static readonly Regex WriteValueRegex = new Regex(@"mem\[(\d+)\] = (\d+)", RegexOptions.Compiled);
        public static IReadOnlyList<WriteValueInstr> ParseWriteValues(string text)
        {
            var result = new List<WriteValueInstr>();
            var lastPattern = new AddressPattern();
            foreach (var line in text.Split('\n').Where(l => l.Trim().Length > 0))
            {
                var newMaskMatch = NewMaskRegex.Match(line);
                if (newMaskMatch.Success)
                {
                    lastPattern = new AddressPattern(newMaskMatch.Groups[1].Value);
                    continue;
                }

                var writeValueMatch = WriteValueRegex.Match(line);
                if (writeValueMatch.Success)
                {
                    result.Add(new WriteValueInstr(
                        lastPattern.Apply(long.Parse(writeValueMatch.Groups[1].Value)),
                        long.Parse(writeValueMatch.Groups[2].Value)));
                    continue;
                }

                throw new InvalidDataException();
            }
            return result;
        }

        public static BigInteger MemorySum(string text) => MemorySum(ParseWriteValues(text));
        public static BigInteger MemorySum(IReadOnlyList<WriteValueInstr> instrs)
        {
            BigInteger result = 0;
            for (int i = 0; i < instrs.Count; i++)
            {
                var pattern = new AddressPattern?(instrs[i].AddressPattern);
                //for (int j = i + 1; j < instrs.Count; j++)
                for (int j = instrs.Count - 1; j > i; j--)
                {
                    pattern = pattern!.Value - instrs[j].AddressPattern;
                    if (pattern == null)
                        break;
                }
                if (pattern != null)
                    result += new BigInteger(pattern.Value.AffectedCount) * instrs[i].Value;
            }
            return result;
        }

        public static void Run()
        {
            Console.WriteLine(MemorySum(File.ReadAllText("day14/input.txt")));
            //1626772765367, too low
            //1627284770229
            //20999001024
        }
    }
}

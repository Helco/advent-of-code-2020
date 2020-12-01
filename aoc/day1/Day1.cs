using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aoc.day1
{
    public static class Day1
    {
        public static void Run()
        {
            var input = File.ReadAllLines("day1/input.txt").Select(l => long.Parse(l));
            var tuple = Find2Sum(input, 2020);
            Console.WriteLine("Multiplied is " + (tuple.Item1 * tuple.Item2));

            var triple = Find3Sum(input, 2020);
            Console.WriteLine("Multiplied is " + (triple.Item1 * triple.Item2 * triple.Item3));
        }

        public static (long, long) Find2Sum(IEnumerable<long> input, long output) =>
            Find2SumInSorted(input.OrderBy(m => m).ToArray(), output) ??
            throw new InvalidOperationException("Could not find a pair");

        public static (long, long)? Find2SumInSorted(IEnumerable<long> sorted, long output)
        {
            for (int j = sorted.Count() - 1; j >= 0; j--)
                for (int i = 0; i < j; i++)
                {
                    long cur = sorted.ElementAt(i) + sorted.ElementAt(j);
                    if (cur == output)
                        return (sorted.ElementAt(i), sorted.ElementAt(j));
                    else if (cur > 2020)
                        break;
                }

            return null;
        }

        public static (long, long, long) Find3Sum(IEnumerable<long> input, long output) =>
            Find3SumInSorted(input.OrderBy(m => m).ToArray(), output) ??
            throw new InvalidOperationException("Could not find a set");

        public static (long, long, long)? Find3SumInSorted(IEnumerable<long> input, long output)
        {
            if (input.Count() < 3)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < input.Count(); i++)
            {
                long curNumber = input.ElementAt(i);
                var newInput = input.Take(i).Concat(input.Skip(i + 1));
                var optPair = Find2SumInSorted(newInput, output - curNumber);
                if (optPair != null)
                    return (curNumber, optPair.Value.Item1, optPair.Value.Item2);
            }
            return null;
        }
    }
}

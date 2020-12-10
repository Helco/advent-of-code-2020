using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace aoc.day10
{
    public static class Day10
    {
        public static IEnumerable<int> FindAdapterChain(IEnumerable<int> allAdapters, int start = 0, int maxDiff = 3)
        {
            var adapters = allAdapters
                .OrderBy(a => a)
                .SkipWhile(a => a - start > maxDiff)
                .ToArray();
            if (adapters.Length < 2)
                return allAdapters;
            for (int i = 1; i < adapters.Length; i++)
            {
                if (adapters[i] - adapters[i - 1] > maxDiff)
                    return adapters.Take(i);
            }
            return adapters;
        }

        public static IEnumerable<int> FindDifferences(IEnumerable<int> allAdapters, int start = 0, int maxDiff = 3)
        {
            var adapters = FindAdapterChain(allAdapters, start, maxDiff).ToArray();
            return adapters
                .Prepend(start)
                .SkipLast(1)
                .Select((a, i) => adapters[i] - a);
        }

        public static long WeirdDifferenceProp(IEnumerable<int> allAdapters, int start = 0, int maxDiff = 3) =>
            FindDifferences(allAdapters, start, maxDiff)
            .Append(3)
            .GroupBy(diff => diff)
            .Where(group => group.Key != 2 && group.Key != 0)
            .Select(group => group.Count())
            .Product();

        public static long CountArrangements(IEnumerable<int> allAdapters, int maxDiff = 3)
        {
            var adapters = allAdapters.Prepend(0).Append(allAdapters.Max() + maxDiff).OrderBy(a => a).ToArray();
            var chainsTo = Enumerable.Repeat((long)0, adapters.Length).ToArray();
            chainsTo[0] = 1;

            for (int i = 0; i < adapters.Length; i++)
            {
                for (int j = 1; j < adapters.Length; j++)
                {
                    if (i + j >= adapters.Length || adapters[i + j] - adapters[i] > maxDiff)
                        break;
                    chainsTo[i + j] += chainsTo[i];
                }
            }
            return chainsTo.Last();
        }

        public static void Run()
        {
            var inputAdapters = File.ReadAllLines("day10/input.txt").Select(l => int.Parse(l)).ToArray();

            Console.WriteLine(WeirdDifferenceProp(inputAdapters));
            Console.WriteLine(CountArrangements(inputAdapters));
        }
    }
}

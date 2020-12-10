using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace aoc.day9
{
    public class XMAS : IEnumerable<long>
    {
        public readonly int WindowSize;
        public List<long> AllNumbers = new List<long>();
        public IEnumerable<long> Window => AllNumbers.TakeLast(WindowSize);

        public XMAS(int windowSize) => WindowSize = windowSize;

        public bool Add(long newNumber)
        {
            var result = Window.Count() < WindowSize || FindSumInWindow(newNumber) != null;
            AllNumbers.Add(newNumber);
            return result;
        }

        /// <summary>Returns the indices of the invalid numbers</summary>
        public IEnumerable<int> AddRange(params long[] numbers)
        {
            var invalid = new List<int>();
            foreach (var number in numbers)
            {
                if (!Add(number))
                    invalid.Add(AllNumbers.Count - 1);
            }
            return invalid;
        }

        /// <summary>Returns the optional pair of <b>indices</b></summary>
        public (int, int)? FindSumInWindow(long newNumber)
        {
            for (int i = 0; i < WindowSize - 1; i++)
            {
                for (int j = i + 1; j < WindowSize; j++)
                {
                    if (Window.ElementAt(i) + Window.ElementAt(j) == newNumber)
                        return (i, j);
                }
            }
            return null;
        }

        public IEnumerable<long>? FindContiguousSum(long newNumber)
        {
            for (int i = 0; i < AllNumbers.Count; i++)
            {
                for (int j = i + 1; j < AllNumbers.Count; j++)
                {
                    var range = AllNumbers.Skip(i).Take(j - i + 1);
                    if (range.Sum() == newNumber)
                        return range;
                }
            }
            return null;
        }

        public long FindWeakness(long number)
        {
            var range = FindContiguousSum(number);
            if (range == null)
                throw new InvalidProgramException("Could not find contiguous sum");
            return range.Min() + range.Max();
        }

        public long this[int index] => AllNumbers[index];
        public IEnumerator<long> GetEnumerator() => AllNumbers.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public static class Day9
    {
        public static void Run()
        {
            var inputNumbers = File.ReadAllLines("day9/input.txt").Select(l => long.Parse(l)).ToArray();

            var xmas = new XMAS(25);
            var firstInvalid = xmas[xmas.AddRange(inputNumbers).First()];
            Console.WriteLine(firstInvalid);
            Console.WriteLine(xmas.FindWeakness(firstInvalid));
        }
    }
}

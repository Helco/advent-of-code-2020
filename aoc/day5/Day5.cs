using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc.day5
{
    public struct Seat
    {
        public readonly int Row;
        public readonly int Column;
        public int ID => Row * 8 + Column;

        private static int ConvertBitString(string txt, char off, char on) => txt
            .Select(ch => ch switch
            {
                _ when ch == off => 0,
                _ when ch == on => 1,
                _ => throw new ArgumentException()
            }).Aggregate(0, (prev, cur) => (prev << 1) | cur);

        public Seat(string txt)
        {
            if (txt.Length != 10)
                throw new ArgumentException();
            Row = ConvertBitString(txt.Substring(0, 7), 'F', 'B');
            Column = ConvertBitString(txt.Substring(7, 3), 'L', 'R');
        }

        public Seat(int id)
        {
            Row = id / 8;
            Column = id % 8;
        }

        public override string ToString() => $"Row: {Row} Column: {Column} ID: {ID}";
    }

    public static class Day5
    {
        public static void Run()
        {
            var seats = File.ReadAllLines("day5/input.txt").Select(l => new Seat(l)).ToArray();

            int min = seats.Min(s => s.ID);
            int max = seats.Max(s => s.ID);
            Console.WriteLine($"Min: {min}\tMax: {max}");

            Console.WriteLine("Free seats: ");
            var seatSet = new HashSet<int>(seats.Select(s => s.ID));
            for (int i = min; i <= max; i++)
            {
                if (!seatSet.Contains(i))
                    Console.WriteLine(new Seat(i));
            }

        }
    }
}

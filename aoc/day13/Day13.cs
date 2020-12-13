using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace aoc.day13
{
    public class BusSchedule
    {
        public readonly IReadOnlyList<int?> Busses;

        public IEnumerable<int> InServiceBusses => Busses
            .Except(new[] { null as int? })
            .Select(b => b!.Value);
        
        public BusSchedule(string schedule)
        {
            Busses = schedule
                .Split(',')
                .Select(b => b == "x" ? null as int? : int.Parse(b))
                .ToArray();
        }

        public static int NextDepartFor(int bus, int ts)
        {
            int lastDepart = ts - (ts % bus);
            return lastDepart + (lastDepart == ts ? 0 : bus);
        }

        public (int busID, int nextDepart) FindNextDepart(int ts) => InServiceBusses
            .Select(bus => (bus, nextDepart: NextDepartFor(bus, ts)))
            .OrderBy(pair => pair.nextDepart)
            .First();

        public int Answer1(int ts)
        {
            var pair = FindNextDepart(ts);
            return pair.busID * (pair.nextDepart - ts);
        }

        public long FindMagicTime()
        {
            var pair = Busses
                .Select((b, i) => (phase: -i, period: b))
                .Where(t => t.period.HasValue)
                .Select(t => (phase: new BigInteger(t.phase), period: new BigInteger(t.period!.Value)))
                .Aggregate((b0, b1) => AoCBigMath.FindSynced(b0.phase, b0.period, b1.phase, b1.period)!.Value);

            return (long)(pair.phase < 0 ? pair.phase + pair.period : pair.phase);
        }
    }

    public static class Day13
    {
        public static void Run()
        {
            var inputLines = File.ReadAllLines("day13/input.txt");
            int ts = int.Parse(inputLines[0]);
            var schedule = new BusSchedule(inputLines[1]);

            Console.WriteLine(schedule.Answer1(ts));
            Console.WriteLine(schedule.FindMagicTime());
        }
    }
}

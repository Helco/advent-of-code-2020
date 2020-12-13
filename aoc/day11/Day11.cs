using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace aoc.day11
{
    public enum SeatState : byte
    {
        Floor,
        Empty,
        Occupied
    }

    public class SeatMap
    {
        public readonly IVec2 Size;
        public readonly IReadOnlyList<SeatState> Seats;

        public SeatMap(string input)
        {
            var lines = input.Trim().Split('\n').Select(l => l.Trim()).Where(l => l.Length > 0).ToArray();
            Size = new IVec2(lines[0].Length, lines.Length);
            Seats = lines.SelectMany(l => l.Select(c => c switch
                {
                    '.' => SeatState.Floor,
                    'L' => SeatState.Empty,
                    '#' => SeatState.Occupied,
                    _ => throw new InvalidDataException()
                })).ToArray();
        }

        public SeatMap(SeatMap prev, bool newRules = false)
        {
            Size = prev.Size;
            Seats = prev.Seats.Select((prevState, i) =>
            {
                int neighborCount = newRules ? prev.CountNeighborsNew(i) : prev.CountNeighbors(i);
                switch(prevState)
                {
                    case SeatState.Floor: return SeatState.Floor;
                    case SeatState.Empty:
                        if (neighborCount == 0)
                            return SeatState.Occupied;
                        break;
                    case SeatState.Occupied:
                        if (neighborCount >= (newRules ? 5 : 4))
                            return SeatState.Empty;
                        break;

                    default: throw new NotImplementedException();
                }
                return prevState;
            }).ToArray();
        }

        public int CountNeighbors(int index) => Neighborhood(index).Count(i => Seats[i] == SeatState.Occupied);

        private IEnumerable<int> Neighborhood(int index)
        {
            if (index < 0 || index >= Seats.Count)
                yield break;
            var pos = new IVec2(index % Size.x, index / Size.x);
            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    var cur = pos + IVec2.Right * x + IVec2.Down * y;
                    if (cur != pos && cur.x >= 0 && cur.x < Size.x && cur.y >= 0 && cur.y < Size.y)
                        yield return (cur.x + cur.y * Size.x);
                }
            }
        }

        private readonly IReadOnlyList<IVec2> Directions = new[]
        {
            new IVec2(-1, -1),
            new IVec2(0, -1),
            new IVec2(+1, -1),
            new IVec2(-1, +1),
            new IVec2(0, +1),
            new IVec2(+1, +1),
            new IVec2(-1, 0),
            new IVec2(+1, 0),
        };
        private int CountNeighborsNew(int index)
        {
            var pos = new IVec2(index % Size.x, index / Size.x);
            int count = 0;
            foreach (var dir in Directions)
            {
                var cur = pos + dir;
                while (cur.x >= 0 && cur.x < Size.x && cur.y >= 0 && cur.y < Size.y)
                {
                    var curState = Seats[cur.x + cur.y * Size.x];
                    if (curState == SeatState.Empty)
                        break;
                    if (curState == SeatState.Occupied)
                    {
                        count++;
                        break;
                    }
                    cur += dir;
                }
            }
            return count;
        }

        public static bool operator ==(SeatMap a, SeatMap b) => a.Size == b.Size && a.Seats.SequenceEqual(b.Seats);
        public static bool operator !=(SeatMap a, SeatMap b) => !(a == b);
        public override bool Equals(object? obj) => obj is SeatMap ? this == (SeatMap)obj : false;
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash ^ Size.GetHashCode()) * 16777619;
                foreach (var seat in Seats)
                    hash = (hash ^ seat.GetHashCode()) * 16777619;
                return hash;
            }
        }
    }

    public static class Day11
    {
        public static SeatMap UntilStabilized(SeatMap map, bool newRules = false)
        {
            var history = new HashSet<SeatMap>();
            while(true)
            {
                var newMap = new SeatMap(map, newRules);
                if (!history.Add(newMap))
                    return map;
                map = newMap;
            }
        }

        public static void Run()
        {
            var inputMap = new SeatMap(File.ReadAllText("day11/input.txt"));
            var stabilized = UntilStabilized(inputMap);
            Console.WriteLine(stabilized.Seats.Count(s => s == SeatState.Occupied));
            var stabilizedNew = UntilStabilized(inputMap, true);
            Console.WriteLine(stabilizedNew.Seats.Count(s => s == SeatState.Occupied));
        }
    }
}

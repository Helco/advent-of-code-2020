using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc.day3
{
    public enum Tile : byte
    {
        Open,
        Tree
    }

    public class Map
    {
        public readonly int Width, Height;
        public readonly Tile[] Tiles;

        public Map(string input)
        {
            var lines = input.Split('\n').Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            Width = lines[0].Trim().Length;
            Height = lines.Length;
            Tiles = lines.SelectMany(l => l.Trim()).Select(ch => ch switch
                {
                    '.' => Tile.Open,
                    '#' => Tile.Tree,
                    _ => throw new InvalidOperationException()
                }).ToArray();
        }

        public Tile this[IVec2 pos] => Tiles[(pos.x % Width) + Width * (pos.y % Height)]; // what about negative?

        public IEnumerable<(IVec2 pos, Tile tile)> Line(IVec2 start, IVec2 slope)
        {
            var cur = start;
            while (true)
            {
                yield return (cur, this[cur]);
                cur += slope;
            }
        }

        public int CountTilesUntil(IVec2 start, IVec2 slope, int targetY, Tile search)
        {
            return Line(start, slope)
                .TakeWhile(t => t.pos.y < targetY)
                .Count(t => t.tile == search);
        }

        public int CountTreesUntilBottom(IVec2 slope) => CountTilesUntil(IVec2.Zero, slope, Height, Tile.Tree);

        private static readonly IVec2[] StandardSlopes = new[]
        {
            new IVec2(1, 1),
            new IVec2(3, 1),
            new IVec2(5, 1),
            new IVec2(7, 1),
            new IVec2(1, 2)
        };
        public int[] CountTreesUntilBottomForStandardSlopes() => StandardSlopes.Select(CountTreesUntilBottom).ToArray();
    }

    public static class Day3
    {

        public static void Run()
        {
            var inputMap = new Map(File.ReadAllText("day3/input.txt"));

            Console.WriteLine(inputMap.CountTreesUntilBottom(new IVec2(3, 1)));
            Console.WriteLine(inputMap.CountTreesUntilBottomForStandardSlopes().Product());
        }
    }
}

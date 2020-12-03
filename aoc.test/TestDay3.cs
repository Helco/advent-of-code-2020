using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day3;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay3
    {
        private const string ExampleMap = @"
..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#";

        [Test]
        public void Example()
        {
            var map = new Map(ExampleMap);
            Assert.AreEqual(7, map.CountTreesUntilBottom(new IVec2(3, 1)));
        }

        [Test]
        public void Example2()
        {
            var map = new Map(ExampleMap);
            var counts = map.CountTreesUntilBottomForStandardSlopes();
            Assert.AreEqual(new[] { 2, 7, 3, 4, 2 }, counts);
            Assert.AreEqual(336L, counts.Product());
        }
    }
}

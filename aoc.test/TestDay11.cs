using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day11;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay11
    {
        private const string Step0Txt = @"
L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";

        private const string Step1Txt = @"
#.##.##.##
#######.##
#.#.#..#..
####.##.##
#.##.##.##
#.#####.##
..#.#.....
##########
#.######.#
#.#####.##";

        private const string Step2Txt = @"
#.LL.L#.##
#LLLLLL.L#
L.L.L..L..
#LLL.LL.L#
#.LL.LL.LL
#.LLLL#.##
..L.L.....
#LLLLLLLL#
#.LLLLLL.L
#.#LLLL.##";

        private const string Step3Txt = @"
#.##.L#.##
#L###LL.L#
L.#.#..#..
#L##.##.L#
#.##.LL.LL
#.###L#.##
..#.#.....
#L######L#
#.LL###L.L
#.#L###.##";

        private const string Step4Txt = @"
#.#L.L#.##
#LLL#LL.L#
L.L.L..#..
#LLL.##.L#
#.LL.LL.LL
#.LL#L#.##
..L.L.....
#L#LLLL#L#
#.LLLLLL.L
#.#L#L#.##";

        private const string Step5Txt = @"
#.#L.L#.##
#LLL#LL.L#
L.#.L..#..
#L##.##.L#
#.#L.LL.LL
#.#L#L#.##
..L.L.....
#L#L##L#L#
#.LLLLLL.L
#.#L#L#.##";

        [Test]
        public void Example1()
        {
            var map0 = new SeatMap(Step0Txt);
            var map1 = new SeatMap(Step1Txt);
            var map2 = new SeatMap(Step2Txt);
            var map3 = new SeatMap(Step3Txt);
            var map4 = new SeatMap(Step4Txt);
            var map5 = new SeatMap(Step5Txt);
            var incMap1 = new SeatMap(map0);
            var incMap2 = new SeatMap(incMap1);
            var incMap3 = new SeatMap(incMap2);
            var incMap4 = new SeatMap(incMap3);
            var incMap5 = new SeatMap(incMap4);
            var incMap6 = new SeatMap(incMap5);

            Assert.AreEqual(map1, incMap1);
            Assert.AreEqual(map2, incMap2);
            Assert.AreEqual(map3, incMap3);
            Assert.AreEqual(map4, incMap4);
            Assert.AreEqual(map5, incMap5);
            Assert.AreEqual(map5, incMap6);
        }

        [Test]
        public void Example1_Stabilized()
        {
            Assert.AreEqual(37, Day11.UntilStabilized(new SeatMap(Step0Txt)).Seats.Count(s => s == SeatState.Occupied));
        }

        private const string Step1_2Txt = @"
#.##.##.##
#######.##
#.#.#..#..
####.##.##
#.##.##.##
#.#####.##
..#.#.....
##########
#.######.#
#.#####.##";

        private const string Step2_2Txt = @"
#.LL.LL.L#
#LLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLL#
#.LLLLLL.L
#.LLLLL.L#";

        private const string Step3_2Txt = @"
#.L#.##.L#
#L#####.LL
L.#.#..#..
##L#.##.##
#.##.#L.##
#.#####.#L
..#.#.....
LLL####LL#
#.L#####.L
#.L####.L#";

        private const string Step4_2Txt = @"
#.L#.L#.L#
#LLLLLL.LL
L.L.L..#..
##LL.LL.L#
L.LL.LL.L#
#.LLLLL.LL
..L.L.....
LLLLLLLLL#
#.LLLLL#.L
#.L#LL#.L#";

        private const string Step5_2Txt = @"
#.L#.L#.L#
#LLLLLL.LL
L.L.L..#..
##L#.#L.L#
L.L#.#L.L#
#.L####.LL
..#.#.....
LLL###LLL#
#.LLLLL#.L
#.L#LL#.L#";

        private const string Step6_2Txt = @"
#.L#.L#.L#
#LLLLLL.LL
L.L.L..#..
##L#.#L.L#
L.L#.LL.L#
#.LLLL#.LL
..#.L.....
LLL###LLL#
#.LLLLL#.L
#.L#LL#.L#";

        [Test]
        public void Example2()
        {
            var map0 = new SeatMap(Step0Txt);
            var map1 = new SeatMap(Step1_2Txt);
            var map2 = new SeatMap(Step2_2Txt);
            var map3 = new SeatMap(Step3_2Txt);
            var map4 = new SeatMap(Step4_2Txt);
            var map5 = new SeatMap(Step5_2Txt);
            var map6 = new SeatMap(Step6_2Txt);
            var incMap1 = new SeatMap(map0, true);
            var incMap2 = new SeatMap(incMap1, true);
            var incMap3 = new SeatMap(incMap2, true);
            var incMap4 = new SeatMap(incMap3, true);
            var incMap5 = new SeatMap(incMap4, true);
            var incMap6 = new SeatMap(incMap5, true);
            var incMap7 = new SeatMap(incMap6, true);

            Assert.AreEqual(map1, incMap1);
            Assert.AreEqual(map2, incMap2);
            Assert.AreEqual(map3, incMap3);
            Assert.AreEqual(map4, incMap4);
            Assert.AreEqual(map5, incMap5);
            Assert.AreEqual(map6, incMap6);
            Assert.AreEqual(map6, incMap7);
        }

        [Test]
        public void Example2_Stabilized()
        {
            Assert.AreEqual(26, Day11.UntilStabilized(new SeatMap(Step0Txt), true).Seats.Count(s => s == SeatState.Occupied));
        }

    }
}

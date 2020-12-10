using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day10;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay10
    {
        public int[] Example1Adapters = new[]
        {
            16,
            10,
            15,
            5,
            1,
            11,
            7,
            19,
            6,
            12,
            4
        };

        public int[] Example2Adapters = new[]
        {
            28,
            33,
            18,
            42,
            31,
            14,
            46,
            20,
            48,
            47,
            24,
            23,
            49,
            45,
            19,
            38,
            39,
            11,
            1,
            32,
            25,
            35,
            8,
            17,
            7,
            9,
            4,
            2,
            34,
            10,
            3
        };

        [Test]
        public void Example1()
        {
            Assert.AreEqual(new[]
            {
                1, 3, 1, 1, 1, 3, 1, 1, 3, 1, 3
            }, Day10.FindDifferences(Example1Adapters));
        }

        [Test]
        public void Example2()
        {
            Assert.AreEqual(22 * 10, Day10.WeirdDifferenceProp(Example2Adapters));
        }

        [Test]
        public void Part2_Own1()
        {
            Assert.AreEqual(16, Day10.CountArrangements(new[] { 7, 3, 2, 5, 1, 6 }));
        }

        [Test]
        public void Part2_Example1()
        {
            Assert.AreEqual(8, Day10.CountArrangements(Example1Adapters));
        }

        [Test]
        public void Part2_Example2()
        {
            Assert.AreEqual(19208, Day10.CountArrangements(Example2Adapters));
        }
    }
}

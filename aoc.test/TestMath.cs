using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static aoc.AoCMath;

namespace aoc.test
{
    class TestMath
    {
        [Test]
        public void TestGCD()
        {
            // example taken from https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm
            Assert.AreEqual(2, GCD(240, 46, out var s, out var t));
            Assert.AreEqual(-9, s);
            Assert.AreEqual(47, t);
        }

        [Test]
        public void TestFindSynced()
        {
            // examples taken from https://math.stackexchange.com/questions/2218763/how-to-find-lcm-of-two-numbers-when-one-starts-with-an-offset

            Assert.AreEqual((-18, 45), FindSynced(-3, 15, 0, 9));
            Assert.AreEqual((450, 570), FindSynced(-6, 38, 0, 30));

            Assert.AreEqual((6, 12), FindSynced(0, 3, -2, 4));
        }
    }
}

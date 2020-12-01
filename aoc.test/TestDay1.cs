using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc.test
{
    class TestDay1
    {
        [Test]
        public void Example()
        {
            var pair = day1.Day1.Find2Sum(new long[] { 1721, 979, 366, 299, 675, 1456 }, 2020);
            Assert.AreEqual(pair.Item1, 299);
            Assert.AreEqual(pair.Item2, 1721);
        }

        [Test]
        public void Example2()
        {
            var result = day1.Day1.Find3Sum(new long[] { 1721, 979, 366, 299, 675, 1456 }, 2020);
            Assert.AreEqual(366, result.Item1);
            Assert.AreEqual(675, result.Item2);
            Assert.AreEqual(979, result.Item3);
        }
    }
}

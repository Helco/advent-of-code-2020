using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day9;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay9
    {
        private XMAS ExampleXMAS1 => new XMAS(25)
        {
            20,
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
            11, 12, 13, 14, 15, 16, 17, 18, 19,
            21, 22, 23, 24, 25
        };
        private XMAS ExampleXMAS1_Step
        {
            get
            {
                var result = ExampleXMAS1;
                Assert.True(result.Add(45));
                return result;
            }
        }

        private long[] ExampleNumbers2 = new long[]
        {
            35,
            20,
            15,
            25,
            47,
            40,
            62,
            55,
            65,
            95,
            102,
            117,
            150,
            182,
            127,
            219,
            299,
            277,
            309,
            576
        };

        [Test]
        public void Example1()
        {
            Assert.True(ExampleXMAS1.Add(26));
            Assert.True(ExampleXMAS1.Add(49));
            Assert.False(ExampleXMAS1.Add(100));
            Assert.False(ExampleXMAS1.Add(50));

            Assert.True(ExampleXMAS1_Step.Add(26));
            Assert.True(ExampleXMAS1_Step.Add(64));
            Assert.True(ExampleXMAS1_Step.Add(66));
            Assert.False(ExampleXMAS1_Step.Add(65));
        }

        [Test]
        public void Example2()
        {
            var xmas = new XMAS(5);
            Assert.AreEqual(127, xmas[xmas.AddRange(ExampleNumbers2).First()]);
        }

        [Test]
        public void Example3()
        {
            var xmas = new XMAS(5);
            xmas.AddRange(ExampleNumbers2);
            Assert.AreEqual(new[] { 15, 25, 47, 40 }, xmas.FindContiguousSum(127));
            Assert.AreEqual(62, xmas.FindWeakness(127));
        }
    }
}

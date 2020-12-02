using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using aoc.day2;

namespace aoc.test
{
    public class TestDay2
    {
        [Test]
        public void Examples()
        {
            Assert.True(Day2.SplitInputLine("1-3 a: abcde").IsValid);
            Assert.False(Day2.SplitInputLine("1-3 b: cdefg").IsValid);
            Assert.True(Day2.SplitInputLine("2-9 c: ccccccccc").IsValid);
        }

        [Test]
        public void ExamplesNew()
        {
            Assert.True(Day2.SplitInputLine("1-3 a: abcde").IsValidNew);
            Assert.False(Day2.SplitInputLine("1-3 b: cdefg").IsValidNew);
            Assert.False(Day2.SplitInputLine("2-9 c: ccccccccc").IsValidNew);
        }
    }
}

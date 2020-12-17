using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day17;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay17
    {
        private const string ExampleText = @"
.#.
..#
###";

        [Test]
        public void Example()
        {
            Assert.AreEqual(112, Day17.RunNStates(ExampleText, 6).ActiveCells.Count);
            Assert.AreEqual(848, Day17.RunNStates4(ExampleText, 6).ActiveCells.Count);
        }
    }
}

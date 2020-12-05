using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day5;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay5
    {

        private readonly Seat Example1 = new Seat("BFFFBBFRRR");
        private readonly Seat Example2 = new Seat("FFFBBBFRRR");
        private readonly Seat Example3 = new Seat("BBFFBBFRLL");

        [Test]
        public void Examples()
        {
            Assert.AreEqual(70, Example1.Row);
            Assert.AreEqual(7, Example1.Column);
            Assert.AreEqual(567, Example1.ID);

            Assert.AreEqual(14, Example2.Row);
            Assert.AreEqual(7, Example2.Column);
            Assert.AreEqual(119, Example2.ID);

            Assert.AreEqual(102, Example3.Row);
            Assert.AreEqual(4, Example3.Column);
            Assert.AreEqual(820, Example3.ID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day6;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay6
    {
        private const string Example1 = @"
abc

a
b
c

ab
ac

a
a
a
a

b";

        [Test]
        public void TestExample1()
        {
            var groups = Day6.ParseGroups(Example1).ToArray();
            Assert.AreEqual(5, groups.Length);
            Assert.AreEqual(1, groups[0].Persons.Length);
            Assert.AreEqual(3, groups[1].Persons.Length);
            Assert.AreEqual(2, groups[2].Persons.Length);
            Assert.AreEqual(4, groups[3].Persons.Length);
            Assert.AreEqual(1, groups[4].Persons.Length);

            Assert.AreEqual(3, groups[0].DistinctConfirms.Count());
            Assert.AreEqual(3, groups[1].DistinctConfirms.Count());
            Assert.AreEqual(3, groups[2].DistinctConfirms.Count());
            Assert.AreEqual(1, groups[3].DistinctConfirms.Count());
            Assert.AreEqual(1, groups[4].DistinctConfirms.Count());

            Assert.AreEqual(3, groups[0].Consensus.Count());
            Assert.AreEqual(0, groups[1].Consensus.Count());
            Assert.AreEqual(1, groups[2].Consensus.Count());
            Assert.AreEqual(1, groups[3].Consensus.Count());
            Assert.AreEqual(1, groups[4].Consensus.Count());
        }
    }
}

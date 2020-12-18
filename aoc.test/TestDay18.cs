using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day18;
using NUnit.Framework;
using static aoc.day18.Day18;

namespace aoc.test
{
    class TestDay18
    {
        [Test]
        public void Examples()
        {
            Assert.AreEqual(3, Parse("3").Evaluate());
            Assert.AreEqual(3, Parse("(3)").Evaluate());
            Assert.AreEqual(3, Parse("(((3)))").Evaluate());
            Assert.AreEqual(6, Parse("(((3)+1)+1)+1").Evaluate());
            Assert.AreEqual(6, Parse("(3+(1+(1+1)))").Evaluate());
            Assert.AreEqual(6, Parse("3+1+1+1").Evaluate());

            Assert.AreEqual(71, Parse("1 + 2 * 3 + 4 * 5 + 6").Evaluate());
            Assert.AreEqual(51, Parse("1 + (2 * 3) + (4 * (5 + 6))").Evaluate());
            Assert.AreEqual(26, Parse("2 * 3 + (4 * 5)").Evaluate());
            Assert.AreEqual(437, Parse("5 + (8 * 3 + 9 + 3 * 4 * 3)").Evaluate());
            Assert.AreEqual(12240, Parse("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))").Evaluate());
            Assert.AreEqual(13632, Parse("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2").Evaluate());
        }

        [Test]
        public void MyExamplesAdv()
        {
            advanced = true;
            Assert.AreEqual(3, Parse("3").Evaluate());
            Assert.AreEqual(3, Parse("(3)").Evaluate());
            Assert.AreEqual(3, Parse("(((3)))").Evaluate());
            Assert.AreEqual(6, Parse("(((3)+1)+1)+1").Evaluate());
            Assert.AreEqual(6, Parse("(3+(1+(1+1)))").Evaluate());
            Assert.AreEqual(6, Parse("3+1+1+1").Evaluate());
        }

        [Test]
        public void Examples2()
        {
            advanced = true;

            Assert.AreEqual(231, Parse("1 + 2 * 3 + 4 * 5 + 6").Evaluate());
            Assert.AreEqual(51, Parse("1 + (2 * 3) + (4 * (5 + 6))").Evaluate());
            Assert.AreEqual(46, Parse("2 * 3 + (4 * 5)").Evaluate());
            Assert.AreEqual(1445, Parse("5 + (8 * 3 + 9 + 3 * 4 * 3)").Evaluate());
            Assert.AreEqual(669060, Parse("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))").Evaluate());
            Assert.AreEqual(23340, Parse("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2").Evaluate());
        }
    }
}

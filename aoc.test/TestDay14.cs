using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day14.p1;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay14
    {
        private const string Example1 = @"
mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";

        [Test]
        public void TestBitmask()
        {
            Assert.AreEqual(0b1001001, new BitMask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X").Apply(0b1011));
            Assert.AreEqual(0b1100101, new BitMask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X").Apply(0b1100101));
            Assert.AreEqual(0b1000000, new BitMask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X").Apply(0));
        }

        [Test]
        public void TestExample1()
        {
            var finalState = Day14.ParseInstructions(Example1).Aggregate(State.Initial, (state, instr) => instr.Apply(state));
            Assert.AreEqual(2, finalState.Memory.Count);
            Assert.AreEqual(101, finalState.Memory[7]);
            Assert.AreEqual(64, finalState.Memory[8]);
            Assert.AreEqual(165, finalState.Sum);
        }
    }
}

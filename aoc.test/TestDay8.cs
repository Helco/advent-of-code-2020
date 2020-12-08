using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day8;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay8
    {
        private const string ExampleProgram1 = @"
nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";

        [Test]
        public void Example1()
        {
            var program = Day8.ParseProgram(ExampleProgram1);
            Assert.AreEqual(5, Day8.RunUntilInfinite(program).Acc);
        }

        [Test]
        public void Example2()
        {
            var program = Day8.ParseProgram(ExampleProgram1);
            Assert.AreEqual(8, Day8.FixProgramByMutation(program));
        }
    }
}

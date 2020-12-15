using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day14.p2;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay14P2
    {
        [Test]
        public void TestAffected()
        {
            Assert.AreEqual(1, new AddressPattern("001110101").AffectedCount);
            Assert.AreEqual(2, new AddressPattern("0011X0101").AffectedCount);
            Assert.AreEqual(4, new AddressPattern("0011X010X").AffectedCount);
            Assert.AreEqual(8, new AddressPattern("X011X010X").AffectedCount);
        }

        [Test]
        public void TestToString()
        {
            Assert.AreEqual("", new AddressPattern("000000000").ToString());
            Assert.AreEqual("1110101", new AddressPattern("001110101").ToString());
            Assert.AreEqual("11X0101", new AddressPattern("0011X0101").ToString());
            Assert.AreEqual("X1X010X", new AddressPattern("00X1X010X").ToString());
            Assert.AreEqual("X011X010X", new AddressPattern("X011X010X").ToString());
        }

        [Test]
        public void TestReduce()
        {
            Assert.AreEqual("XX0010X", (new AddressPattern("XX0010X") - "0110XXX").ToString());
            Assert.AreEqual("100010X", (new AddressPattern("XX0010X") - "0100XXX").ToString());
            Assert.AreEqual(null, new AddressPattern("010010X") - "0100XXX");

            Assert.AreEqual(null, (new AddressPattern("000000") - "XXXXXX")?.ToString());
            Assert.AreEqual("111111", (new AddressPattern("XXXXXX") - "000000")?.ToString());
            Assert.AreEqual("", (new AddressPattern("XXXXXX") - "111111")?.ToString());
            Assert.AreEqual(null, (new AddressPattern("110011") - "110011")?.ToString());
            Assert.AreEqual(null, (new AddressPattern("110011") - "110XX1")?.ToString());
            Assert.AreEqual("111111", (new AddressPattern("11XX11") - "110011")?.ToString());
            Assert.AreEqual("110011", (new AddressPattern("11XX11") - "111111")?.ToString());
            Assert.AreEqual(null, (new AddressPattern("XXXXXX") - "XXXXXX")?.ToString());
            Assert.AreEqual(null, (new AddressPattern("000000") - "000000")?.ToString());
            Assert.AreEqual(null, (new AddressPattern("111111") - "111111")?.ToString());

            Assert.AreEqual("XX0000", (new AddressPattern("XX0000") - "X01010")?.ToString());
            Assert.AreEqual("X10000", (new AddressPattern("XX0000") - "X00000")?.ToString());
            
            Assert.AreEqual(null, (new AddressPattern("0") - "0")?.ToString());
            Assert.AreEqual("", (new AddressPattern("0") - "1")?.ToString());
            Assert.AreEqual(null, (new AddressPattern("0") - "X")?.ToString());
            Assert.AreEqual("1", (new AddressPattern("1") - "0")?.ToString());
            Assert.AreEqual(null, (new AddressPattern("1") - "1")?.ToString());
            Assert.AreEqual(null, (new AddressPattern("1") - "X")?.ToString());
            Assert.AreEqual("1", (new AddressPattern("X") - "0")?.ToString());
            Assert.AreEqual("", (new AddressPattern("X") - "1")?.ToString());
            Assert.AreEqual(null, (new AddressPattern("X") - "X")?.ToString());
        }

        /*[Test]
        public void TestReduce3()
        {
            int length = 4;
            int count = 3;

            for (int i = 0; i < (int)Math.Pow(3, length * count); i++)
            {
                var patternChars = Enumerable.Repeat('-', length * count).ToArray();
                int k = i;
                for (int j = 0; j < length * count; j++)
                {
                    int l = k % 3;
                    k /= 3;
                    patternChars[j] = l == 0 ? 'X' : l == 1 ? '0' : '1';
                }
                var patterns = new AddressPattern[count];
                for (int j = 0; j < count; j++)
                    patterns[j] = new AddressPattern(new string(patternChars.Skip(j * length).Take(length).ToArray()));

                var result1 = new AddressPattern?(patterns[0]);
                for (int j = 1; j < count; j++)
                    result1 -= patterns[j];

                var result2 = new AddressPattern?(patterns[0]);
                for (int j = count - 1; j > 0; j--)
                    result2 -= patterns[j];

                Assert.AreEqual(result1, result2, string.Join(" - ", patterns));
            }
        }*/

        [Test]
        public void TestApply()
        {
            Assert.AreEqual("X1101X", (new AddressPattern("X1001X").Apply(0b101010)).ToString());
            Assert.AreEqual("1X0XX", (new AddressPattern("X0XX").Apply(0b11010)).ToString());
            Assert.AreEqual("111XX", (new AddressPattern("0011XX").Apply(0b010101)).ToString());
        }

        private const string Example1 = @"
mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";

        [Test]
        public void TestExample1()
        {
            Assert.AreEqual(208, (long)Day14.MemorySum(Example1));
        }
    }
}

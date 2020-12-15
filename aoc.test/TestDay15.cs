using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day15;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay15
    {
        [Test]
        public void Example1()
        {
            var game = new MemoryGame(new[] { 0, 3, 6 }).PlayUntil(10);
            Assert.AreEqual(new[] { 0, 3, 6, 0, 3, 3, 1, 0, 4, 0 }, game.SpokenNumbers);
            Assert.AreEqual(10, game.LastTurn);
        }

        [Test]
        public void OtherExamples1()
        {
            Assert.AreEqual(436, new MemoryGame(new[] { 0, 3, 6 }).PlayUntil(2020).LastNumber);
            Assert.AreEqual(1, new MemoryGame(new[] { 1, 3, 2 }).PlayUntil(2020).LastNumber);
            Assert.AreEqual(10, new MemoryGame(new[] { 2, 1, 3 }).PlayUntil(2020).LastNumber);
            Assert.AreEqual(27, new MemoryGame(new[] { 1, 2, 3 }).PlayUntil(2020).LastNumber);
            Assert.AreEqual(78, new MemoryGame(new[] { 2, 3, 1 }).PlayUntil(2020).LastNumber);
            Assert.AreEqual(438, new MemoryGame(new[] { 3, 2, 1 }).PlayUntil(2020).LastNumber);
            Assert.AreEqual(1836, new MemoryGame(new[] { 3, 1, 2 }).PlayUntil(2020).LastNumber);
        }

        [Test]
        public void OtherExamples2()
        {
            // Takes too much time to always run
            /*Assert.AreEqual(175594, new MemoryGame(new[] { 0, 3, 6 }).PlayUntil(30000000).LastNumber);
            Assert.AreEqual(2578, new MemoryGame(new[] { 1, 3, 2 }).PlayUntil(30000000).LastNumber);
            Assert.AreEqual(3544142, new MemoryGame(new[] { 2, 1, 3 }).PlayUntil(30000000).LastNumber);
            Assert.AreEqual(261214, new MemoryGame(new[] { 1, 2, 3 }).PlayUntil(30000000).LastNumber);
            Assert.AreEqual(6895259, new MemoryGame(new[] { 2, 3, 1 }).PlayUntil(30000000).LastNumber);
            Assert.AreEqual(18, new MemoryGame(new[] { 3, 2, 1 }).PlayUntil(30000000).LastNumber);
            Assert.AreEqual(362, new MemoryGame(new[] { 3, 1, 2 }).PlayUntil(30000000).LastNumber);*/
        }
    }
}

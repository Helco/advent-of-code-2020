using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day13;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay13
    {
        private const string Example1 = "7,13,x,x,59,x,31,19";

        [Test]
        public void NextDepart()
        {
            Assert.AreEqual(945, BusSchedule.NextDepartFor(7, 939));
            Assert.AreEqual(938, BusSchedule.NextDepartFor(7, 938));
            Assert.AreEqual(949, BusSchedule.NextDepartFor(13, 939));
            Assert.AreEqual(944, BusSchedule.NextDepartFor(59, 939));
        }

        [Test]
        public void Answer1()
        {
            Assert.AreEqual(295, new BusSchedule(Example1).Answer1(939));
        }

        [Test]
        public void FindMagicTime()
        {
            Assert.AreEqual(1068781, new BusSchedule(Example1).FindMagicTime());
            Assert.AreEqual(3417, new BusSchedule("17,x,13,19").FindMagicTime());
            Assert.AreEqual(754018, new BusSchedule("67,7,59,61").FindMagicTime());
            Assert.AreEqual(779210, new BusSchedule("67,x,7,59,61").FindMagicTime());
            Assert.AreEqual(1261476, new BusSchedule("67,7,x,59,61").FindMagicTime());
            Assert.AreEqual(1202161486, new BusSchedule("1789,37,47,1889").FindMagicTime());
        }
    }
}

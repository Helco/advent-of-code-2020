using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day12;
using NUnit.Framework;
using Action = aoc.day12.Action;

namespace aoc.test
{
    class TestDay12
    {
        [Test]
        public void Example1()
        {
            var ship0 = Ship.Initial;
            var ship1 = new Ship(ship0, new Action("F10"));
            var ship2 = new Ship(ship1, new Action("N3"));
            var ship3 = new Ship(ship2, new Action("F7"));
            var ship4 = new Ship(ship3, new Action("R90"));
            var ship5 = new Ship(ship4, new Action("F11"));

            Assert.AreEqual(new IVec2(10, 0), ship1.Pos);
            Assert.AreEqual(new IVec2(10, -3), ship2.Pos);
            Assert.AreEqual(new IVec2(17, -3), ship3.Pos);
            Assert.AreEqual(new IVec2(17, -3), ship4.Pos);
            Assert.AreEqual(new IVec2(17, 8), ship5.Pos);
            Assert.AreEqual(25, ship5.Pos.LengthManhattan);
        }

        [Test]
        public void Example2()
        {
            var ship0 = WaypointShip.Initial;
            var ship1 = new WaypointShip(ship0, new Action("F10"));
            var ship2 = new WaypointShip(ship1, new Action("N3"));
            var ship3 = new WaypointShip(ship2, new Action("F7"));
            var ship4 = new WaypointShip(ship3, new Action("R90"));
            var ship5 = new WaypointShip(ship4, new Action("F11"));

            Assert.AreEqual(new IVec2(100, -10), ship1.ShipPos);
            Assert.AreEqual(new IVec2(100, -10), ship2.ShipPos);
            Assert.AreEqual(new IVec2(170, -38), ship3.ShipPos);
            Assert.AreEqual(new IVec2(170, -38), ship4.ShipPos);
            Assert.AreEqual(new IVec2(214, 72), ship5.ShipPos);

            Assert.AreEqual(286, ship5.ShipPos.LengthManhattan);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aoc.day16;
using NUnit.Framework;

namespace aoc.test
{
    class TestDay16
    {
        private static readonly Field ClassField = new Field("class: 1-3 or 5-7");
        private static readonly Field RowField = new Field("row: 6-11 or 33-44");
        private static readonly Field SeatField = new Field("seat: 13-40 or 45-50");
        private static Field[] ExampleFields;
        private static Ticket ExampleMyTicket;
        private static Ticket[] ExampleNearbyTickets;

        static TestDay16()
        {
            ExampleFields = new[] { ClassField, RowField, SeatField };
            ExampleMyTicket = new Ticket("7,1,14", ExampleFields);
            ExampleNearbyTickets = new[]
            {
                new Ticket("7,3,47", ExampleFields),
                new Ticket("40,4,50", ExampleFields),
                new Ticket("55,2,20", ExampleFields),
                new Ticket("38,6,12", ExampleFields)
            };
        }

        [Test]
        public void TestErrorRate()
        {
            Assert.AreEqual(4, ExampleNearbyTickets[1].ErrorRate);
            Assert.AreEqual(55, ExampleNearbyTickets[2].ErrorRate);
            Assert.AreEqual(12, ExampleNearbyTickets[3].ErrorRate);
            Assert.AreEqual(71, ExampleNearbyTickets.Sum(t => t.ErrorRate));
        }
    }

    class TestDay16P2
    {
        private const string FieldsTxt = @"
class: 0-1 or 4-19
row: 0-5 or 8-19
seat: 0-13 or 16-19";

        private const string NearbyTicketsTxt = @"
3,9,18
15,1,5
5,14,9";

        private const string MyTicketTxt = "11,12,13";

        [Test]
        public void TestPossibleFor()
        {
            var fields = FieldsTxt.Split('\n').Where(l => l.Trim().Length > 0).Select(l => new Field(l)).ToArray();
            var nearbyTickets = NearbyTicketsTxt.Split('\n').Where(l => l.Trim().Length > 0).Select(l => new Ticket(l, fields)).ToArray();
            var myTicket = new Ticket(MyTicketTxt, fields);

            Assert.AreEqual(new[] { 0, 1, 2 }, myTicket.PossibleFieldsFor("class"));
            Assert.AreEqual(new[] { 0, 1, 2 }, myTicket.PossibleFieldsFor("row"));
            Assert.AreEqual(new[] { 0, 1, 2 }, myTicket.PossibleFieldsFor("seat"));

            Assert.AreEqual(new[] { 1, 2 }, nearbyTickets[0].PossibleFieldsFor("class"));
            Assert.AreEqual(new[] { 0, 1, 2 }, nearbyTickets[0].PossibleFieldsFor("row"));
            Assert.AreEqual(new[] { 0, 1, 2 }, nearbyTickets[0].PossibleFieldsFor("seat"));

            Assert.AreEqual(new[] { 0, 1, 2 }, nearbyTickets[1].PossibleFieldsFor("class"));
            Assert.AreEqual(new[] { 0, 1, 2 }, nearbyTickets[1].PossibleFieldsFor("row"));
            Assert.AreEqual(new[] { 1, 2 }, nearbyTickets[1].PossibleFieldsFor("seat"));

            Assert.AreEqual(new[] { 0, 1, 2 }, nearbyTickets[2].PossibleFieldsFor("class"));
            Assert.AreEqual(new[] { 0, 1, 2 }, nearbyTickets[2].PossibleFieldsFor("row"));
            Assert.AreEqual(new[] { 0, 2 }, nearbyTickets[2].PossibleFieldsFor("seat"));
        }

        [Test]
        public void TestReduce()
        {
            var fields = FieldsTxt.Split('\n').Where(l => l.Trim().Length > 0).Select(l => new Field(l)).ToArray();
            var nearbyTickets = NearbyTicketsTxt.Split('\n').Where(l => l.Trim().Length > 0).Select(l => new Ticket(l, fields)).Where(t => t.IsValid).ToArray();
            var myTicket = new Ticket(MyTicketTxt, fields);

            Day16.ReduceTickets(nearbyTickets, fields);
            Assert.AreEqual(1, nearbyTickets[0].PossibleFields[0].Count);
            Assert.AreEqual(1, nearbyTickets[0].PossibleFields[1].Count);
            Assert.AreEqual(1, nearbyTickets[0].PossibleFields[2].Count);
            Assert.AreEqual(1, nearbyTickets[1].PossibleFields[0].Count);
            Assert.AreEqual(1, nearbyTickets[1].PossibleFields[1].Count);
            Assert.AreEqual(1, nearbyTickets[1].PossibleFields[2].Count);
            Assert.AreEqual(1, nearbyTickets[2].PossibleFields[0].Count);
            Assert.AreEqual(1, nearbyTickets[2].PossibleFields[1].Count);
            Assert.AreEqual(1, nearbyTickets[2].PossibleFields[2].Count);

        }
    }
}

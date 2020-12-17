using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace aoc.day16
{
    public readonly struct Field
    {
        private static readonly Regex FieldRegex = new Regex(@"(.+?):.+?(\d+)-(\d+).+?(\d+)-(\d+)", RegexOptions.Compiled);
        public readonly string Name;
        public readonly (int Min, int Max) First, Second;

        public Field(string line)
        {
            var match = FieldRegex.Match(line);
            if (!match.Success)
                throw new InvalidDataException();
            Name = match.Groups[1].Value;
            First = (int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
            Second = (int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value));
        }

        public bool IsValid(int value) =>
            (value >= First.Min && value <= First.Max) ||
            (value >= Second.Min && value <= Second.Max);
    }

    public class Ticket
    {
        public readonly IReadOnlyList<int> Values;
        public HashSet<string>[] PossibleFields;

        public Ticket(IEnumerable<int> values, IEnumerable<Field> fields)
        {
            Values = values.ToArray();
            PossibleFields = values
                .Select(value => fields.Where(f => f.IsValid(value)).Select(f => f.Name).ToHashSet())
                .ToArray();
        }
        public Ticket(string line, IEnumerable<Field> fields) : this(line.Split(',').Select(p => int.Parse(p)), fields) { }

        public int ErrorRate => Values.Zip(PossibleFields)
            .Where(t => t.Second.Count == 0)
            .Sum(t => t.First);
        public bool IsValid => ErrorRate == 0;

        public long DepartureValue => Values.Zip(PossibleFields)
            .Where(t => t.Second.Single().Contains("departure"))
            .Select(t => (long)t.First)
            .Product();

        public IEnumerable<int> PossibleFieldsFor(string name) => PossibleFields
            .Select((f, i) => f.Contains(name) ? i : -1)
            .Where(i => i >= 0);

        public int? SinglePossibleFor(string name) => PossibleFieldsFor(name).Count() == 1 ? PossibleFieldsFor(name).Single() : null as int?;

        // true when affected something
        public bool SetOnlyPossible(int fieldI, string name)
        {
            var result = false;
            for (int i = 0; i < PossibleFields.Length; i++)
            {
                if (i == fieldI)
                {
                    result = PossibleFields[i].SequenceEqual(new[] { name }) || result;
                    PossibleFields[fieldI] = new HashSet<string>() { name };
                }
                else
                    result = PossibleFields[i].Remove(name) || result;
            }
            return result;
        }
    }

    public static class Day16
    {
        private static readonly int[] MyTicketNumbers = new[] { 163, 151, 149, 67, 71, 79, 109, 61, 83, 137, 89, 59, 53, 179, 73, 157, 139, 173, 131, 167 };

        public static void ReduceTickets(Ticket[] tickets, Field[] fields)
        {
            var fieldsRemaining = fields.Select(f => f.Name).ToHashSet();
            var fieldsSet = new HashSet<int>();
            bool didSomething;
            do
            {
                /*var onlyPossibleFields = fieldsRemaining
                    .Select(field =>
                    {
                        var possibleFields = Enumerable
                            .Range(0, fields.Length)
                            .Where(i => tickets.All(t => t.PossibleFieldsFor(field).Contains(i)))
                            .ToArray();
                        if (possibleFields.Length == 1)
                            return (name: field, fieldI: possibleFields.Single());

                        return (name: field, fieldI: null as int?);
                    })
                    .Where(t => t.fieldI != null)
                    .GroupBy(t => t.fieldI)
                    .Select(g => (name: g.Count() == 1 ? g.First().name : g.Single(f => tickets.Any(t => t.SinglePossibleFor(f.name) == g.Key)).name, fieldI: g.Key))
                    .ToArray();*/
                var onlyPossibleFields = new List<(string name, int? fieldI)>();
                foreach (var field in fieldsRemaining)
                {
                    var possible = Enumerable.Range(0, fields.Length).ToHashSet();
                    foreach (var ticket in tickets)
                    {
                        var curPossible = ticket.PossibleFieldsFor(field);
                        var noPossible = new List<int>(fieldsSet);
                        foreach (var p in possible)
                        {
                            if (!curPossible.Contains(p))
                                noPossible.Add(p);
                        }
                        foreach (var p in noPossible)
                            possible.Remove(p);
                    }

                    if (possible.Count == 1)
                        onlyPossibleFields.Add((name: field, fieldI: possible.Single()));
                    else if (possible.Count == 0)
                        throw new InvalidDataException();
                }

                didSomething = false;
                foreach (var onlyPossibleField in onlyPossibleFields)
                {
                    foreach (var ticket in tickets)
                        didSomething = ticket.SetOnlyPossible(onlyPossibleField.fieldI!.Value, onlyPossibleField.name) || didSomething;
                    fieldsRemaining.Remove(onlyPossibleField.name);
                    fieldsSet.Add(onlyPossibleField.fieldI!.Value);
                    Console.WriteLine($"{onlyPossibleField.fieldI}: {onlyPossibleField.name}");
                }
            } while (didSomething);
        }

        public static void Run()
        {
            var fields = File.ReadAllLines("day16/fields.txt").Select(l => new Field(l)).ToArray();
            var myTicket = new Ticket(MyTicketNumbers, fields);
            var nearbyTickets = File.ReadAllLines("day16/tickets.txt").Select(l => new Ticket(l, fields)).ToArray();
            var validTickets = nearbyTickets.Where(t => t.IsValid).Append(myTicket).ToArray();

            Console.WriteLine(nearbyTickets.Sum(t => t.ErrorRate));

            ReduceTickets(validTickets, fields);
            Console.WriteLine(myTicket.DepartureValue);
        }
    }
}

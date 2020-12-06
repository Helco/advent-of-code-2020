using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc.day6
{
    public class Person
    {
        public readonly IReadOnlySet<char> Confirms;

        public Person(string line)
        {
            Confirms = line.Trim().ToHashSet();
        }
    }

    public class Group
    {
        public readonly Person[] Persons;
        public Group(IEnumerable<string> lines)
        {
            Persons = lines.Select(l => new Person(l)).ToArray();
        }

        public IReadOnlySet<char> DistinctConfirms => Persons
            .SelectMany(p => p.Confirms)
            .Distinct()
            .ToHashSet();

        public IReadOnlySet<char> Consensus => DistinctConfirms
            .Where(ch => Persons.All(p => p.Confirms.Contains(ch)))
            .ToHashSet();
    }

    public static class Day6
    {
        public static IEnumerable<Group> ParseGroups(string input)
        {
            var lines = input.Split('\n');
            var curPart = new List<string>();
            foreach (var line in lines)
            {
                if (line.Trim().Length == 0)
                {
                    if (curPart.Any())
                        yield return new Group(curPart);
                    curPart = new List<string>();
                }
                else
                    curPart.Add(line);
            }
            if (curPart.Any())
                yield return new Group(curPart);

        }

        public static void Run()
        {
            var input = File.ReadAllText("day6/input.txt");
            var groups = ParseGroups(input).ToArray();

            Console.WriteLine(groups.Sum(g => g.DistinctConfirms.Count));
            Console.WriteLine(groups.Sum(g => g.Consensus.Count));
        }
    }
}

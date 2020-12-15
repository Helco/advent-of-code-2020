using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace aoc.day15
{
    public class MemoryGame
    {
        public List<int> SpokenNumbers = new List<int>();
        public Dictionary<int, int> LastTurnByNumber = new Dictionary<int, int>();
        public int LastTurn => SpokenNumbers.Count;
        public int LastNumber => SpokenNumbers.Last();

        public MemoryGame(IReadOnlyList<int> initialNumbers)
        {
            SpokenNumbers = initialNumbers.ToList();
            for (int i = 0; i < initialNumbers.Count; i++)
                LastTurnByNumber[initialNumbers[i]] = i + 1;
        }

        public MemoryGame PlayUntil(int turns)
        {
            while (LastTurn < turns)
                NextTurn();
            return this;
        }

        public MemoryGame NextTurn()
        {
            int prevLastNumber = LastNumber;
            SpokenNumbers.Add(LastTurnByNumber.TryGetValue(LastNumber, out var spokenAt)
                ? LastTurn - spokenAt
                : 0);
            LastTurnByNumber[prevLastNumber] = LastTurn - 1;
            return this;
        }
    }

    public static class Day15
    {
        public static readonly int[] Input = new[] { 2, 20, 0, 4, 1, 17 };

        public static void Run()
        {
            Console.WriteLine(new MemoryGame(Input).PlayUntil(2020).LastNumber);
            Console.WriteLine(new MemoryGame(Input).PlayUntil(30000000).LastNumber);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace aoc.day17
{
    public class State
    {
        public readonly IReadOnlySet<IVec3> ActiveCells;

        public HashSet<IVec3> PotentialNextCells => ActiveCells
            .Concat(ActiveCells.SelectMany(c => c.MooreNeighborhood()))
            .ToHashSet();

        public State(State prevState)
        {
            var activeCells = new HashSet<IVec3>();
            foreach (var cur in prevState.PotentialNextCells)
            {
                int neighbors = prevState.NeighborsAt(cur);
                bool wasActive = prevState.ActiveCells.Contains(cur);
                if ((wasActive && neighbors >= 2 && neighbors <= 3) ||
                    (!wasActive && neighbors == 3))
                    activeCells.Add(cur);
            }
            ActiveCells = activeCells;
        }

        public State(string initialText)
        {
            var activeCells = new HashSet<IVec3>();
            var lines = initialText.Split('\n').Select(l => l.Trim()).Where(l => l.Length > 0).ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                        activeCells.Add(new IVec3(i, j, 0));
                }
            }
            ActiveCells = activeCells;
        }

        public int NeighborsAt(IVec3 cell) => cell
            .MooreNeighborhood()
            .Count(n => ActiveCells.Contains(n));
    }

    public class State4
    {
        public readonly IReadOnlySet<IVec4> ActiveCells;

        public HashSet<IVec4> PotentialNextCells => ActiveCells
            .Concat(ActiveCells.SelectMany(c => c.MooreNeighborhood()))
            .ToHashSet();

        public State4(State4 prevState)
        {
            var activeCells = new HashSet<IVec4>();
            foreach (var cur in prevState.PotentialNextCells)
            {
                int neighbors = prevState.NeighborsAt(cur);
                bool wasActive = prevState.ActiveCells.Contains(cur);
                if ((wasActive && neighbors >= 2 && neighbors <= 3) ||
                    (!wasActive && neighbors == 3))
                    activeCells.Add(cur);
            }
            ActiveCells = activeCells;
        }

        public State4(string initialText)
        {
            var activeCells = new HashSet<IVec4>();
            var lines = initialText.Split('\n').Select(l => l.Trim()).Where(l => l.Length > 0).ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                        activeCells.Add(new IVec4(i, j, 0, 0));
                }
            }
            ActiveCells = activeCells;
        }

        public int NeighborsAt(IVec4 cell) => cell
            .MooreNeighborhood()
            .Count(n => ActiveCells.Contains(n));
    }

    public static class Day17
    {
        public static State RunNStates(string initial, int cycles)
        {
            var state = new State(initial);
            for (int i = 0; i < cycles; i++)
                state = new State(state);
            return state;
        }

        public static State4 RunNStates4(string initial, int cycles)
        {
            var state = new State4(initial);
            for (int i = 0; i < cycles; i++)
                state = new State4(state);
            return state;
        }

        public static void Run()
        {
            var inputTxt = File.ReadAllText("day17/input.txt");
            Console.WriteLine(RunNStates(inputTxt, 6).ActiveCells.Count);
            Console.WriteLine(RunNStates4(inputTxt, 6).ActiveCells.Count);
        }
    }
}

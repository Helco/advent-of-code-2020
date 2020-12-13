using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc.day8
{
    public enum Op
    {
        acc,
        jmp,
        nop
    }

    public struct Instruction
    {
        public readonly Op Op;
        public readonly int Arg;

        private static readonly Regex InstructionRegex = new Regex(@"(nop|acc|jmp) ([+-]?\d+)");
        public Instruction(string line)
        {
            var match = InstructionRegex.Match(line.Trim());
            Op = match.Groups[1].Value switch
            {
                nameof(Op.acc) => Op.acc,
                nameof(Op.jmp) => Op.jmp,
                nameof(Op.nop) => Op.nop,
                var opName => throw new InvalidDataException($"Unknown operation {opName}")
            };
            Arg = int.Parse(match.Groups[2].Value);
        }

        public Instruction(Op op, int arg)
        {
            Op = op;
            Arg = arg;
        }
    }

    public struct State
    {
        public readonly IReadOnlyList<Instruction> Program;
        public readonly int IP; // of the next instruction
        public readonly int Acc;

        public State(IReadOnlyList<Instruction> program)
        {
            IP = Acc = 0;
            Program = program;
        }

        public State(State prevState)
        {
            Program = prevState.Program;
            IP = prevState.IP;
            Acc = prevState.Acc;

            var instr = Program[prevState.IP];
            switch (instr.Op)
            {
                case Op.nop: IP++; break;
                case Op.acc:
                    Acc += instr.Arg;
                    IP++;
                    break;
                case Op.jmp:
                    IP += instr.Arg;
                    break;

                default: throw new NotImplementedException();
            }
        }
    }

    public class CompleteStateComparer : IEqualityComparer<State>
    {
        public bool Equals(State a, State b) =>
            a.Program == b.Program &&
            a.IP == b.IP &&
            a.Acc == b.Acc;

        public int GetHashCode([DisallowNull] State obj)
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash ^ obj.Program.GetHashCode()) * 16777619;
                hash = (hash ^ obj.IP.GetHashCode()) * 16777619;
                hash = (hash ^ obj.Acc.GetHashCode()) * 16777619;
                return hash;
            }
        }
    }
    

    public class IPStateComparer : IEqualityComparer<State>
    {
        public bool Equals(State a, State b) =>
            a.Program == b.Program &&
            a.IP == b.IP;

        public int GetHashCode([DisallowNull] State obj)
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash ^ obj.Program.GetHashCode()) * 16777619;
                hash = (hash ^ obj.IP.GetHashCode()) * 16777619;
                return hash;
            }
        }
    }

    public static class Day8
    {
        public static IReadOnlyList<Instruction> ParseProgram(string input) => input
            .Split('\n')
            .Where(l => l.Trim().Length > 0)
            .Select(l => new Instruction(l))
            .ToArray();

        public static State RunUntilInfinite(IReadOnlyList<Instruction> program)
        {
            var state = new State(program);
            var history = new HashSet<State>(new IPStateComparer());
            while (true)
            {
                var newState = new State(state);
                if (!history.Add(newState))
                    return state;
                state = newState;
            }
        }

        public static IEnumerable<IReadOnlyList<Instruction>> Mutations(IReadOnlyList<Instruction> program)
        {
            for (int i = 0; i < program.Count; i++)
            {
                Op? newOp = program[i].Op switch
                {
                    Op.nop => Op.jmp,
                    Op.jmp => Op.nop,
                    _ => null
                };
                if (newOp == null)
                    continue;

                yield return
                    program.Take(i).Append(
                        new Instruction(newOp.Value, program[i].Arg)).Concat(
                        program.Skip(i + 1)).ToArray();
            }
        }

        public static int? TryRunUntilFinished(IReadOnlyList<Instruction> program)
        {
            var state = new State(program);
            var history = new HashSet<State>(new IPStateComparer());
            while (true)
            {
                var newState = new State(state);
                if (newState.IP == program.Count)
                    return newState.Acc;
                if (!history.Add(newState))
                    return null;
                state = newState;
            }
        }

        public static int FixProgramByMutation(IReadOnlyList<Instruction> original) =>
            Mutations(original)
            .Select(mutated => TryRunUntilFinished(mutated))
            .First(result => result != null) ??
            throw new InvalidDataException("Program cannot be fixed by mutation");

        public static void Run()
        {
            var inputProgram = ParseProgram(File.ReadAllText("day8/input.txt"));
            Console.WriteLine(RunUntilInfinite(inputProgram).Acc);
            Console.WriteLine(FixProgramByMutation(inputProgram));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace aoc.day14.p1
{
    public readonly struct BitMask
    {
        public readonly long OverrideMask; // where 1 means keep original and 0 means override
        public readonly long OverrideValues;

        public BitMask(string line)
        {
            line = line.Trim();
            if (line.Length != 36)
                throw new ArgumentException();

            OverrideMask = line.Select(c => c == 'X' ? 1L : 0L).Aggregate((prev, cur) => prev << 1 | cur);
            OverrideValues = line.Select(c => c == '1' ? 1L : 0L).Aggregate((prev, cur) => prev << 1 | cur);
        }

        public long Apply(long value) => (value & OverrideMask) | (OverrideValues);
    }

    public readonly struct State
    {
        public readonly IReadOnlyDictionary<long, long> Memory;
        public readonly BitMask Mask;

        public State(IReadOnlyDictionary<long, long> memory, BitMask mask) => (Memory, Mask) = (memory, mask);

        public static State Initial => new State(new Dictionary<long, long>(), new BitMask());

        public long Sum => Memory.Values.Sum();
    }

    public interface IInstruction
    {
        State Apply(State prevState);
    }

    public readonly struct NewMaskInstr : IInstruction
    {
        public readonly BitMask newMask;

        public NewMaskInstr(string line) => newMask = new BitMask(line);

        public State Apply(State prevState) => new State(prevState.Memory, newMask);
    }

    public readonly struct WriteValueInstr : IInstruction
    {
        public readonly long Address;
        public readonly long Value;

        public WriteValueInstr(long address, long value) => (Address, Value) = (address, value);

        public State Apply(State prevState)
        {
            var newMemory = new Dictionary<long, long>(prevState.Memory);
            newMemory[Address] = prevState.Mask.Apply(Value);
            return new State(newMemory, prevState.Mask);
        }
    }

    public static class Day14
    {
        private static readonly Regex NewMaskRegex = new Regex(@"mask = ([X10]{36})", RegexOptions.Compiled);
        private static readonly Regex WriteValueRegex = new Regex(@"mem\[(\d+)\] = (\d+)", RegexOptions.Compiled);
        public static IInstruction ParseInstruction(string line)
        {
            var newMaskMatch = NewMaskRegex.Match(line);
            if (newMaskMatch.Success)
                return new NewMaskInstr(newMaskMatch.Groups[1].Value);

            var writeValueMatch = WriteValueRegex.Match(line);
            if (writeValueMatch.Success)
                return new WriteValueInstr(long.Parse(writeValueMatch.Groups[1].Value), long.Parse(writeValueMatch.Groups[2].Value));

            throw new InvalidDataException();
        }

        public static IInstruction[] ParseInstructions(string text) => text
            .Split('\n')
            .Where(l => l.Trim().Length > 0)
            .Select(ParseInstruction)
            .ToArray();

        public static void Run()
        {
            var inputInstructions = ParseInstructions(File.ReadAllText("day14/input.txt"));

            var finalState = inputInstructions.Aggregate(State.Initial, (state, instr) => instr.Apply(state));
            Console.WriteLine(finalState.Sum);
        }
    }
}

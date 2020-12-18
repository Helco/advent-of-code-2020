using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace aoc.day18
{
    public interface IExpression
    {
        long Evaluate();
    }

    public readonly struct Constant : IExpression
    {
        public readonly long Value;

        public Constant(char c) => Value = long.Parse("" + c);
        public Constant(string s) => Value = long.Parse(s);
        public Constant(long v) => Value = v;
        public long Evaluate() => Value;
    }

    public enum BinaryOp
    {
        Addition,
        Multiplication
    }

    public readonly struct BinaryExpression : IExpression
    {
        public readonly IExpression Left, Right;
        public readonly BinaryOp Op;

        public BinaryExpression(BinaryOp op, IExpression left, IExpression right) => (Op, Left, Right) = (op, left, right);

        public long Evaluate() => Op switch
        {
            BinaryOp.Addition => Left.Evaluate() + Right.Evaluate(),
            BinaryOp.Multiplication => Left.Evaluate() * Right.Evaluate(),
            _ => throw new NotImplementedException()
        };

        public IExpression ConvertRightFirstToLeftFirst(BinaryOp? forOp = null)
        {
            var parts = new List<(BinaryOp, IExpression)>();

            var lastOp = Op;
            var right = Right;
            while (right is BinaryExpression && (forOp == null || ((BinaryExpression)right).Op == forOp))
            {
                var rightBin = (BinaryExpression)right;
                parts.Add((lastOp, rightBin.Left));
                lastOp = rightBin.Op;
                right = rightBin.Right;
            }
            parts.Add((lastOp, right));

            return parts.Aggregate(Left, (left, part) => new BinaryExpression(part.Item1, left, part.Item2));
        }
    }

    public readonly struct BracketedExpression : IExpression
    {
        public readonly IExpression Inner;

        public BracketedExpression(IExpression inner) => Inner = inner;

        public long Evaluate() => Inner.Evaluate();
    }

    public enum TokenType
    {
        EndOfInput,
        BracketOpen,
        BracketClose,
        Constant,
        Addition,
        Multiplication
    }

    public readonly struct Token
    {
        public readonly TokenType Type;
        public readonly string Value;

        public Token(TokenType t, char c = '\0') => (Type, Value) = (t, "" + c);
    }

    public static class Day18
    {
        public static IEnumerable<Token> Scan(string input)
        {
            input = input.Replace(" ", "").Trim();
            return input.Select(c => c switch
            {
                _ when c >= '0' && c <= '9' => new Token(TokenType.Constant, c),
                '(' => new Token(TokenType.BracketOpen),
                ')' => new Token(TokenType.BracketClose),
                '+' => new Token(TokenType.Addition),
                '*' => new Token(TokenType.Multiplication),
                _ => throw new InvalidDataException()
            });
        }

        public static bool advanced = false;

        public static IExpression Parse(string input) => Parse(Scan(input));
        public static IExpression Parse(IEnumerable<Token> tokens)
        {
            var scanner = tokens.GetEnumerator();
            if (!scanner.MoveNext())
                throw new InvalidDataException();
            return Expression(scanner);

            IExpression Expression(IEnumerator<Token> scanner) => scanner.Current.Type switch
            {
                TokenType.BracketOpen => advanced ? AdvBinaryExpression(scanner) : BinaryExpression(scanner),
                TokenType.Constant => advanced ? AdvBinaryExpression(scanner) : BinaryExpression(scanner),
                _ => throw new InvalidDataException()
            };

            IExpression BracketedExpression(IEnumerator<Token> scanner)
            {
                if (scanner.Current.Type != TokenType.BracketOpen)
                    throw new InvalidOperationException();
                scanner.MoveNext();
                var result = Expression(scanner);
                if (scanner.Current.Type != TokenType.BracketClose)
                    throw new InvalidOperationException();
                scanner.MoveNext();
                return new BracketedExpression(result);
            }

            IExpression BinaryExpression(IEnumerator<Token> scanner)
            {
                var left = ConstantExpression(scanner);
                IExpression right;
                while(true)
                {
                    switch(scanner.Current.Type)
                    {
                        case TokenType.Addition:
                            scanner.MoveNext();
                            right = ConstantExpression(scanner);
                            left = new BinaryExpression(BinaryOp.Addition, left, right);
                            break;
                        case TokenType.Multiplication:
                            scanner.MoveNext();
                            right = ConstantExpression(scanner);
                            left = new BinaryExpression(BinaryOp.Multiplication, left, right);
                            break;
                        default:
                            return left is BinaryExpression ? ((BinaryExpression)left).ConvertRightFirstToLeftFirst() : left;
                    }
                }
            }

            IExpression AdvBinaryExpression(IEnumerator<Token> scanner)
            {
                var left = SubAdvBinaryExpression(scanner);
                IExpression right;
                while(true)
                {
                    switch(scanner.Current.Type)
                    {
                        case TokenType.Multiplication:
                            scanner.MoveNext();
                            right = SubAdvBinaryExpression(scanner);
                            left = new BinaryExpression(BinaryOp.Multiplication, left, right);
                            break;
                        default:
                            return left is BinaryExpression ? ((BinaryExpression)left).ConvertRightFirstToLeftFirst(BinaryOp.Multiplication) : left;
                    }
                }
            }

            IExpression SubAdvBinaryExpression(IEnumerator<Token> scanner)
            {
                var left = ConstantExpression(scanner);
                IExpression right;
                while(true)
                {
                    switch(scanner.Current.Type)
                    {
                        case TokenType.Addition:
                            scanner.MoveNext();
                            right = ConstantExpression(scanner);
                            left = new BinaryExpression(BinaryOp.Addition, left, right);
                            break;
                        default:
                            return left is BinaryExpression ? ((BinaryExpression)left).ConvertRightFirstToLeftFirst(BinaryOp.Addition) : left;
                    }
                }
            }

            IExpression ConstantExpression(IEnumerator<Token> scanner)
            {
                switch (scanner.Current.Type)
                {
                    case TokenType.Constant:
                        var result = new Constant(scanner.Current.Value);
                        scanner.MoveNext();
                        return result;
                    case TokenType.BracketOpen:
                        return BracketedExpression(scanner);
                    default:
                        throw new InvalidDataException();
                }
            }
        }

        public static void Run()
        {
            var inputExprs = File.ReadAllLines("day18/input.txt").Select(i => Parse(i)).ToArray();
            Console.WriteLine(inputExprs.Select(e => e.Evaluate()).Sum());

            advanced = true;
            inputExprs = File.ReadAllLines("day18/input.txt").Select(i => Parse(i)).ToArray();
            Console.WriteLine(inputExprs.Select(e => e.Evaluate()).Sum());
        }
    }
}

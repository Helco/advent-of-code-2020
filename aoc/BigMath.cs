using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace aoc
{
    public static class AoCBigMath
    {
        /// <summary>Computes <c>gcd(a,b) = sa + tb</c></summary>
        /// <remarks>Taken from https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm#Pseudocode </remarks>
        public static BigInteger GCD(BigInteger a, BigInteger b, out BigInteger outS, out BigInteger outT)
        {
            var (prevR, r) = (a, b);
            var (prevS, s) = (BigInteger.One, BigInteger.Zero);
            var (prevT, t) = (BigInteger.Zero, BigInteger.One);

            while (r != 0)
            {
                var quotient = prevR / r;
                (prevR, r) = (r, prevR - quotient * r);
                (prevS, s) = (s, prevS - quotient * s);
                (prevT, t) = (t, prevT - quotient * t);
            }

            outS = prevS;
            outT = prevT;
            return prevR;
        }

        public static BigInteger GCD(BigInteger a, BigInteger b) => GCD(a, b, out var _, out var _);

        public static BigInteger LCM(BigInteger a, BigInteger b) => BigInteger.Abs(a * b) / GCD(a, b); // TODO: overflow check?

        /// <summary>Finds a combined integer periodic system</summary>
        /// <remarks>Taken from https://math.stackexchange.com/questions/2218763/how-to-find-lcm-of-two-numbers-when-one-starts-with-an-offset </remarks>
        public static (BigInteger phase, BigInteger period)? FindSynced(BigInteger phaseA, BigInteger periodA, BigInteger phaseB, BigInteger periodB)
        {
            var g = GCD(periodA, periodB, out var s, out var t);
            if ((phaseA - phaseB) % g != 0)
                return null;
            var z = (phaseA - phaseB) / g;
            var m = z * s;

            var periodC = LCM(periodA, periodB);
            var phaseC = (-m * periodA + phaseA) % periodC;
            return (phaseC, periodC);
        }
    }
}

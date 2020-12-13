using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc
{
    public static class AoCMath
    {
        /// <summary>Computes <c>gcd(a,b) = sa + tb</c></summary>
        /// <remarks>Taken from https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm#Pseudocode </remarks>
        public static long GCD(long a, long b, out long outS, out long outT)
        {
            var (prevR, r) = (a, b);
            var (prevS, s) = (1L, 0L);
            var (prevT, t) = (0L, 1L);

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

        public static long GCD(long a, long b) => GCD(a, b, out var _, out var _);

        public static long LCM(long a, long b) => Math.Abs(a * b) / GCD(a, b); // TODO: overflow check?

        /// <summary>Finds a combined integer periodic system</summary>
        /// <remarks>Taken from https://math.stackexchange.com/questions/2218763/how-to-find-lcm-of-two-numbers-when-one-starts-with-an-offset </remarks>
        public static (long phase, long period)? FindSynced(long phaseA, long periodA, long phaseB, long periodB)
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

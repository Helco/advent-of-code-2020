using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc
{
    public static class EnumerableExtensions
    {
        public static long Product(this IEnumerable<int> set) => set.Aggregate(1L, (prev, cur) => prev * cur);
        public static long Product(this IEnumerable<long> set) => set.Aggregate(1L, (prev, cur) => prev * cur);
    }
}

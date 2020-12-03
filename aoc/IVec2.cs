using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc
{
    public struct IVec2
    {
        public static readonly IVec2 Zero = new IVec2(0, 0);
        public static readonly IVec2 One = new IVec2(1, 1);
        public static readonly IVec2 Right = new IVec2(1, 0);
        public static readonly IVec2 Left = new IVec2(-1, 0);
        public static readonly IVec2 Up = new IVec2(0, -1);
        public static readonly IVec2 Down = new IVec2(0, 1);

        public readonly int x, y;

        public IVec2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public IVec2 WithX(int newX) => new IVec2(newX, y);
        public IVec2 WithY(int newY) => new IVec2(x, newY);

        public static IVec2 operator +(IVec2 a, IVec2 b) => new IVec2(a.x + b.x, a.y + b.y);
        public static IVec2 operator -(IVec2 a, IVec2 b) => new IVec2(a.x - b.x, a.y - b.y);
        public static IVec2 operator *(IVec2 a, int s) => new IVec2(a.x * s, a.y * s);
    }
}

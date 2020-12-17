using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc
{
    public readonly struct IVec4
    {
        public static readonly IVec4 Zero = new IVec4(0, 0, 0, 0);
        public static readonly IVec4 One = new IVec4(1, 1, 1, 0);
        public static readonly IVec4 Right = new IVec4(1, 0, 0, 0);
        public static readonly IVec4 Left = new IVec4(-1, 0, 0, 0);
        public static readonly IVec4 Up = new IVec4(0, -1, 0, 0);
        public static readonly IVec4 Down = new IVec4(0, 1, 0, 0);
        public static readonly IVec4 Forward = new IVec4(0, 0, 1, 0);
        public static readonly IVec4 Backward = new IVec4(0, 0, -1, 0);

        public readonly int x, y, z, w;

        public IVec4(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override bool Equals(object? obj) => (obj is IVec4) ? ((IVec4)obj) == this : false;

        public override int GetHashCode() =>  unchecked(((((((((int)2166136261 ^ 16777619) * x) ^ 16777619) * y) ^ 16777619) * z) ^ 16777619) * w ^ 16777619);

        public override string ToString() => $"[{x},{y},{z},{w}]";

        public IVec4 WithX(int newX) => new IVec4(newX, y, z, w);
        public IVec4 WithY(int newY) => new IVec4(x, newY, z, w);
        public IVec4 WithZ(int newZ) => new IVec4(x, y, newZ, w);
        public IVec4 WithW(int newW) => new IVec4(x, y, z, newW);

        public static IVec4 operator +(IVec4 a, IVec4 b) => new IVec4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static IVec4 operator -(IVec4 a, IVec4 b) => new IVec4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        public static IVec4 operator *(IVec4 a, int s) => new IVec4(a.x * s, a.y * s, a.z * s, a.w * s);
        public static IVec4 operator /(IVec4 a, int s) => new IVec4(a.x / s, a.y / s, a.z / s, a.w / s);
        public static bool operator ==(IVec4 a, IVec4 b) => a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
        public static bool operator !=(IVec4 a, IVec4 b) => a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w;

        public IVec4 Normalized => this / (int)AoCMath.GCD(AoCMath.GCD(AoCMath.GCD(x, y), z), w);
        public int Dot(IVec4 a) => x * a.x + y * a.y + z * a.z + w * a.w;
        public int LengthSqr => Dot(this);
        public int Length => (int)Math.Sqrt(LengthSqr);
        public int LengthManhattan => Math.Abs(x) + Math.Abs(y) + Math.Abs(z) + Math.Abs(w);

        public IEnumerable<IVec4> MooreNeighborhood()
        {
            for (int x = -1; x < 2; x++)
                for (int y = -1; y < 2; y++)
                    for (int z = -1; z < 2; z++)
                        for (int w = -1; w < 2; w++)
                            if (x != 0 || y != 0 || z != 0 || w != 0)
                                yield return this + new IVec4(x, y, z, w);
        }
    }
}

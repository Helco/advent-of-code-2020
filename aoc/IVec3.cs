using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc
{
    public readonly struct IVec3
    {
        public static readonly IVec3 Zero = new IVec3(0, 0, 0);
        public static readonly IVec3 One = new IVec3(1, 1, 1);
        public static readonly IVec3 Right = new IVec3(1, 0, 0);
        public static readonly IVec3 Left = new IVec3(-1, 0, 0);
        public static readonly IVec3 Up = new IVec3(0, -1, 0);
        public static readonly IVec3 Down = new IVec3(0, 1, 0);
        public static readonly IVec3 Forward = new IVec3(0, 0, 1);
        public static readonly IVec3 Backward = new IVec3(0, 0, -1);

        public readonly int x, y, z;

        public IVec3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(object? obj) => (obj is IVec3) ? ((IVec3)obj) == this : false;

        public override int GetHashCode() =>  unchecked((((((((int)2166136261 ^ 16777619) * x) ^ 16777619) * y) ^ 16777619) * z) ^ 16777619);

        public override string ToString() => $"[{x},{y},{z}]";

        public IVec3 WithX(int newX) => new IVec3(newX, y, z);
        public IVec3 WithY(int newY) => new IVec3(x, newY, z);
        public IVec3 WithZ(int newZ) => new IVec3(x, y, newZ);

        public static IVec3 operator +(IVec3 a, IVec3 b) => new IVec3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static IVec3 operator -(IVec3 a, IVec3 b) => new IVec3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static IVec3 operator *(IVec3 a, int s) => new IVec3(a.x * s, a.y * s, a.z * s);
        public static IVec3 operator /(IVec3 a, int s) => new IVec3(a.x / s, a.y / s, a.z / s);
        public static bool operator ==(IVec3 a, IVec3 b) => a.x == b.x && a.y == b.y && a.z == b.z;
        public static bool operator !=(IVec3 a, IVec3 b) => a.x != b.x || a.y != b.y || a.z != b.z;

        public IVec3 Normalized => this / (int)AoCMath.GCD(AoCMath.GCD(x, y), z);
        public int Dot(IVec3 a) => x * a.x + y * a.y + z * a.z;
        public int LengthSqr => Dot(this);
        public int Length => (int)Math.Sqrt(LengthSqr);
        public int LengthManhattan => Math.Abs(x) + Math.Abs(y) + Math.Abs(z);

        public IEnumerable<IVec3> MooreNeighborhood()
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    for (int z = -1; z < 2; z++)
                    {
                        if (x != 0 || y != 0 || z != 0)
                            yield return this + new IVec3(x, y, z);
                    }
                }
            }
        }
    }
}

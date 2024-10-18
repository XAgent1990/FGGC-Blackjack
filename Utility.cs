using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Godot;

namespace FGGCBlackJack {
    public enum Suit {
        Spade, Heart, Club, Diamond
    }

    public enum CardValue {
        Ace = 11,
        v02 = 2,
        v03 = 3,
        v04 = 4,
        v05 = 5,
        v06 = 6,
        v07 = 7,
        v08 = 8,
        v09 = 9,
        v10 = 10,
        Jack = 20,
        Queen = 30,
        King = 40
    }

    public enum Axis {
        x, y, z
    }

    [Serializable]
    public struct IntVector2 {
        public int x, y;
        public IntVector2(int x, int y) {
            this.x = x;
            this.y = y;
        }
        public static IntVector2 operator +(IntVector2 v1, IntVector2 v2) => new(v1.x + v2.x, v1.y + v2.y);
        public static IntVector2 operator -(IntVector2 v1, IntVector2 v2) => new(v1.x - v2.x, v1.y - v2.y);
        public static IntVector2 operator *(IntVector2 v1, IntVector2 v2) => new(v1.x * v2.x, v1.y * v2.y);
        public static IntVector2 operator /(IntVector2 v1, IntVector2 v2) => new(v1.x / v2.x, v1.y / v2.y);
        public static IntVector2 operator *(IntVector2 v1, float factor) => new(
                Mathf.RoundToInt(v1.x * factor),
                Mathf.RoundToInt(v1.y * factor)
            );
        public static IntVector2 operator *(float factor, IntVector2 v1) => v1 * factor;
        public static IntVector2 operator /(IntVector2 v1, float divisor) => new(
                Mathf.RoundToInt(v1.x / divisor),
                Mathf.RoundToInt(v1.y / divisor)
            );
        public static bool operator ==(IntVector2 v1, IntVector2 v2) => v1.x == v2.x && v1.y == v2.y;
        public static bool operator !=(IntVector2 v1, IntVector2 v2) => !(v1 == v2);
        public override readonly bool Equals(object o) {
            if (o.GetType() != typeof(IntVector2)) return false;
            return (IntVector2)o == this;
        }
        public override readonly int GetHashCode() => base.GetHashCode();
    }

    [Serializable]
    public struct IntVector3 {
        public int x, y, z;
        public IntVector3(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public static IntVector3 operator +(IntVector3 v1, IntVector3 v2) => new(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        public static IntVector3 operator -(IntVector3 v1, IntVector3 v2) => new(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        public static IntVector3 operator *(IntVector3 v1, IntVector3 v2) => new(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        public static IntVector3 operator /(IntVector3 v1, IntVector3 v2) => new(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
        public static IntVector3 operator *(IntVector3 v1, float factor) => new(
                Mathf.RoundToInt(v1.x * factor),
                Mathf.RoundToInt(v1.y * factor),
                Mathf.RoundToInt(v1.z * factor)
            );
        public static IntVector3 operator *(float factor, IntVector3 v1) => v1 * factor;
        public static IntVector3 operator /(IntVector3 v1, float divisor) => new(
                Mathf.RoundToInt(v1.x / divisor),
                Mathf.RoundToInt(v1.y / divisor),
                Mathf.RoundToInt(v1.z / divisor)
            );
        public static bool operator ==(IntVector3 v1, IntVector3 v2) => v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        public static bool operator !=(IntVector3 v1, IntVector3 v2) => !(v1 == v2);
        public override readonly bool Equals(object o) {
            if (o.GetType() != typeof(IntVector3))
                return false;
            return (IntVector3)o == this;
        }
        public override readonly int GetHashCode() => base.GetHashCode();
    }

    [Serializable]
    public struct Vector3 {
        public float X, Y, Z;
        public Vector3(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }
        public override readonly string ToString() => $"({X}, {Y}, {Z})";
        public static readonly Vector3 Zero = new(0, 0, 0);
        public static readonly Vector3 One = new(1, 1, 1);
        public static readonly Vector3 UnitX = new(1, 0, 0);
        public static readonly Vector3 UnitY = new(0, 1, 0);
        public static readonly Vector3 UnitZ = new(0, 0, 1);

        public override readonly bool Equals([NotNullWhen(true)] object obj) =>
            typeof(Vector3) == obj.GetType() && (Vector3)obj == this;

        public override readonly int GetHashCode() => base.GetHashCode();

        public static bool operator ==(Vector3 v1, Vector3 v2) => v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        public static bool operator !=(Vector3 v1, Vector3 v2) => !(v1 == v2);
        public static Vector3 operator +(Vector3 v1) => v1;
        public static Vector3 operator -(Vector3 v1) => new(-v1.X, -v1.Y, -v1.Z);
        public static Vector3 operator +(Vector3 v1, Vector3 v2) =>
            new(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        public static Vector3 operator -(Vector3 v1, Vector3 v2) => v1 + (-v2);
        public static float operator *(Vector3 v1, Vector3 v2) =>
            v1.X * v2.X + (-v1.Y) * v2.Y + v1.Z * v2.Z;
        public static Vector3 operator *(Vector3 v, float f) => new(v.X * f, v.Y * f, v.Z * f);
        public static Vector3 operator *(float f, Vector3 v) => v * f;
        public static Vector3 operator /(Vector3 v, float f) => new(v.X / f, v.Y / f, v.Z / f);

        public static implicit operator Godot.Vector3(Vector3 v) => new(v.X, v.Y, v.Z);
        public static implicit operator Vector3(Godot.Vector3 v) => new(v.X, v.Y, v.Z);

        public readonly float Length { get => Mathf.Sqrt(X * X + Y * Y + Z * Z); }
        public readonly Vector3 Normalized { get => Length == 0 ? Zero : this / Length; }

        public readonly Vector3 Abs() => new(X.Abs(), Y.Abs(), Z.Abs());
        public void Normalize() => this = Normalized;

        public static float Distance(Vector3 v1, Vector3 v2) => (v1 - v2).Length;
        public readonly float Distance(Vector3 v) => (this - v).Length;
        public static float Angle(Vector3 v1, Vector3 v2) => Mathf.Acos(v1 * v2 / v1.Length / v2.Length);
        public readonly float Angle(Vector3 v) => Mathf.Acos(this * v / Length / v.Length);
    }

    public static class Utility {
        public static void SortKeyValue(ref string[] keys, ref int[] values) {
            BubblesortKeyValue(ref keys, ref values);
        }

        public static void BubblesortKeyValue(ref string[] keys, ref int[] values) {
            for (int i = keys.Length - 1; i > 0; i--) {
                for (int j = 0; j < i; j++) {
                    if (values[j] < values[j + 1]) {
                        (values[j + 1], values[j]) = (values[j], values[j + 1]);
                        (keys[j + 1], keys[j]) = (keys[j], keys[j + 1]);
                    }
                }
            }
        }
        
        public static T Instantiate<T>(string path) where T : Node =>
            ResourceLoader.Load<PackedScene>(path).Instantiate<T>();
    }

    public static class Extensions {
        public static char AsString(this Axis axis) {
            switch (axis) {
                case Axis.x:
                    return 'x';
                case Axis.y:
                    return 'y';
                case Axis.z:
                    return 'z';
                default:
                    return '-';
            }
        }

        public static float Map(this float value, float fromMin, float fromMax, float toMin, float toMax) {
            float centerFrom = (fromMin + fromMax) / 2;
            float centerTo = (toMin + toMax) / 2;
            float delta = centerTo - centerFrom;
            float sizeFrom = fromMax - fromMin;
            float sizeTo = toMax - toMin;
            float factor = sizeTo / sizeFrom;
            return value = (value + delta) * factor;
        }

        public static float Rounded(this float value, float roundToNearest) =>
            Mathf.Round(value / roundToNearest) * roundToNearest;

        public static Vector3 Rounded(this Vector3 value, float roundToNearest) =>
            new(
                Mathf.Round(value.X / roundToNearest) * roundToNearest,
                Mathf.Round(value.Y / roundToNearest) * roundToNearest,
                Mathf.Round(value.Z / roundToNearest) * roundToNearest
            );

        public static void Toggle(this ref bool value) => value = !value;
        public static void Trash(this Node node) => node.QueueFree();

        public static int Abs(this int value) => Mathf.Abs(value);

        public static float Abs(this float value) => Mathf.Abs(value);

        public static char ToChar(this bool value) => value ? 'T' : 'F';

        public static float HorizontalDistance(this Vector3 posA, Vector3 posB) =>
            Vector3.Distance(new Vector3(posA.X, 0, posA.Z), new Vector3(posB.X, 0, posB.Z));

        public static Vector3 ClosestAxisDirection(this Vector3 vector) {
            switch (vector.MaxAxis()) {
                case Axis.x:
                    if (vector.X > 0)
                        return Vector3.UnitX;
                    else
                        return -Vector3.UnitX;
                case Axis.y:
                    if (vector.Y > 0)
                        return Vector3.UnitY;
                    else
                        return -Vector3.UnitY;
                case Axis.z:
                    if (vector.Z > 0)
                        return Vector3.UnitZ;
                    else
                        return -Vector3.UnitZ;
                default:
                    return Vector3.Zero;
            }
        }

        public static Axis MaxAxis(this Vector3 vector) {
            Vector3 abs = vector.Abs();
            float max = Math.Max(abs.X, abs.Y, abs.Z);
            if (max == abs.X)
                return Axis.x;
            if (max == abs.Y)
                return Axis.y;
            return Axis.z;
        }

        public static Vector3 Abs(this Vector3 vector) => new(vector.X.Abs(), vector.Y.Abs(), vector.Z.Abs());
    }

    public static class Math {
        public static float Min(params float[] args) {
            float min = float.MaxValue;
            foreach (float value in args) {
                if (value < min)
                    min = value;
            }
            return min;
        }

        public static float Max(params float[] args) {
            float max = float.MinValue;
            foreach (float value in args) {
                if (value > max)
                    max = value;
            }
            return max;
        }

        public static float Confine(float value, float min, float max) => Max(Min(value, max), min);

        public static int Random(int min, int max, int seed) =>
            new RandomNumberGenerator() { Seed = (ulong)seed }.RandiRange(min, max);
    }
}
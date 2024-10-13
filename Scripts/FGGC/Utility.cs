using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Godot;

namespace FGGCBlackJack.Scripts.FGGC
{
    public enum Axis
    {
        x, y, z
    }

    [Serializable]
    public struct IntVector2
    {
        public int x, y;
        public IntVector2(int x, int y)
        {
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
        public override readonly bool Equals(object o)
        {
            if (o.GetType() != typeof(IntVector2)) return false;
            return (IntVector2)o == this;
        }
        public override readonly int GetHashCode() => base.GetHashCode();
    }

    [Serializable]
    public struct IntVector3
    {
        public int x, y, z;
        public IntVector3(int x, int y, int z)
        {
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
        public override readonly bool Equals(object o)
        {
            if (o.GetType() != typeof(IntVector3))
                return false;
            return (IntVector3)o == this;
        }
        public override readonly int GetHashCode() => base.GetHashCode();
    }

    [Serializable]
    public struct Vector3{
        public float X, Y, Z;
        public Vector3(float x, float y, float z){
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

        public readonly float Length {get => Mathf.Sqrt(X * X + Y * Y + Z * Z);}
        public readonly Vector3 Normalized {get => Length == 0 ? Zero : this / Length;}

        public readonly Vector3 Abs() => new(X.Abs(), Y.Abs(), Z.Abs());
        public void Normalize() => this = Normalized;

        public static float Distance(Vector3 v1, Vector3 v2) => (v1-v2).Length;
        public readonly float Distance(Vector3 v) => (this-v).Length;
        public static float Angle(Vector3 v1, Vector3 v2) => Mathf.Acos(v1 * v2 / v1.Length / v2.Length);
        public readonly float Angle(Vector3 v) => Mathf.Acos(this * v / Length / v.Length);
    } 

    public static class Utility
    {
        // public static bool IsMouseOverUI() {
        //     return EventSystem.current.IsPointerOverGameObject();
        // }

        public static void SortKeyValue(ref string[] keys, ref int[] values)
        {
            //QuicksortKeyValue(ref keys, ref values, 0, keys.Length - 1);
            BubblesortKeyValue(ref keys, ref values);
        }

        public static void BubblesortKeyValue(ref string[] keys, ref int[] values)
        {
            for (int i = keys.Length - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (values[j] < values[j + 1])
                    {
                        int value = values[j];
                        values[j] = values[j + 1];
                        values[j + 1] = value;
                        string key = keys[j];
                        keys[j] = keys[j + 1];
                        keys[j + 1] = key;
                    }
                }
            }
        }

        // public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), Quaternion localRotation = default(Quaternion), Vector3 localScale = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000) {
        //     if(color == null) {
        //         color = Color.white;
        //     }
        //     return CreateWorldText(parent, text, localPosition, localRotation, localScale, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
        // }

        //     public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, Quaternion localRotation, Vector3 localScale, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder) {
        //         GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        //         Transform transform = gameObject.transform;
        //         transform.localRotation = localRotation;
        //         transform.localPosition = localPosition;
        //         transform.localScale = localScale;
        //         transform.SetParent(parent, true);
        //         TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        //         textMesh.anchor = textAnchor;
        //         textMesh.alignment = textAlignment;
        //         textMesh.text = text;
        //         textMesh.fontSize = fontSize;
        //         textMesh.color = color;
        //         textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        //         return textMesh;
        //     }
    }

    public static class Extensions
    {
        public static char AsString(this Axis axis)
        {
            switch (axis)
            {
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

        public static float Map(this float value, float fromMin, float fromMax, float toMin, float toMax)
        {
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

        // public static void Trash(this Node node)
        // {
        //     node.GetParent().RemoveChild(node);
        //     Game.Instance.trash.AddChild(node);
        // }
        // public static void Trash(this Transform transform) => transform.parent = Game.Instance.trash;
        // public static void Trash(this GameObject gameObject) => gameObject.transform.Trash();

        // public static GameObject NewInstance(this GameObject obj, Transform parent = null, string name = null)
        // {
        //     GameObject newObj;
        //     if (parent == null)
        //         newObj = UnityEngine.Object.Instantiate(obj);
        //     else
        //         newObj = UnityEngine.Object.Instantiate(obj, parent);
        //     if (name != null)
        //         newObj.name = name;
        //     return newObj;
        // }

        // public static GameObject NewInstance(this Transform obj, Transform parent = null, string name = null)
        // {
        //     return obj.gameObject.NewInstance(parent, name);
        // }

        // public static GameObject NewInstance(this GameObject obj, string name = null)
        // {
        //     GameObject newObj;
        //     newObj = UnityEngine.Object.Instantiate(obj);
        //     if (name != null)
        //         newObj.name = name;
        //     return newObj;
        // }

        // public static GameObject NewInstance(this Transform transform, string name = null)
        // {
        //     return transform.gameObject.NewInstance(name);
        // }

        // public static Transform GetChild(this GameObject obj, int index) => obj.transform.GetChild(index);

        // public static bool TryGetComponentInParents<T>(this Transform transform, out T component)
        // {
        //     component = default;
        //     Transform p = transform;
        //     while (p.parent != null)
        //     {
        //         p = p.parent;
        //         if (p.TryGetComponent<T>(out component))
        //             return true;
        //     }
        //     return false;
        // }
        // public static bool TryGetComponentInParents<T>(this GameObject obj, out T component)
        // {
        //     return obj.transform.TryGetComponentInParents<T>(out component);
        // }

        // public static void SetActive(this Transform transform, bool value) => transform.gameObject.SetActive(value);
        // public static bool IsActive(this Transform transform) => transform.gameObject.activeSelf;

        // public static void Translate(this GameObject obj, Vector3 translation) => obj.transform.Translate(translation);

        // public static void Translate(this GameObject obj, float x, float y, float z) => obj.transform.Translate(x, y, z);

        // /// <summary>
        // /// Returns a random Child.
        // /// </summary>
        // /// <param name="parent"></param>
        // /// <returns></returns>
        // public static Transform RandomChild(this Transform parent)
        // {
        //     if (parent == null)
        //         return null;
        //     if (parent.childCount == 0)
        //         return null;
        //     if (parent.childCount == 1)
        //         return parent.GetChild(0);
        //     int i = UnityEngine.Random.Range(0, parent.childCount);
        //     return parent.GetChild(i);
        // }

        // /// <summary>
        // /// Returns the GameObject of a random Child of this GameObject.
        // /// </summary>
        // /// <param name="parent"></param>
        // /// <returns></returns>
        // public static GameObject RandomChild(this GameObject parent)
        // {
        //     if (parent == null)
        //         return null;
        //     if (parent.transform.childCount == 0)
        //         return null;
        //     if (parent.transform.childCount == 1)
        //         return parent.transform.GetChild(0).gameObject;
        //     int i = UnityEngine.Random.Range(0, parent.transform.childCount);
        //     return parent.transform.GetChild(i).gameObject;
        // }

        // /// <summary>
        // /// Randomly selects one of the Children, and creates a new instance of it.
        // /// </summary>
        // /// <param name="obj">The GameObject from which a new Instance will be created.</param>
        // /// <param name="parent">Optional Parent to attach the new Instance to.</param>
        // /// <returns></returns>
        // public static GameObject NewVariantInstance(this GameObject obj, Transform parent = null)
        // {
        //    if(obj == null) return null;
        //    GameObject variant = obj.RandomChild();
        //    if(variant == null) return null;
        //    string name = obj.name + "_" + variant.name;
        //    if(parent == null)
        //        return variant.NewInstance(null, name);
        //    else
        //        return variant.NewInstance(parent, name);
        // }

        // public static int ChildCount(this GameObject obj) => obj.transform.childCount;

        // public static Transform[] GetAllChildren(this Transform root)
        // {
        //     List<Transform> children = new();
        //     foreach (Transform child in root.GetComponentsInChildren<Transform>())
        //         if (child != root)
        //             children.Add(child);
        //     return children.ToArray();
        // }
        // public static Transform[] GetAllChildren(this GameObject root) => root.transform.GetAllChildren();
        // public static Transform[] GetAllChildren(this Transform root, string name)
        // {
        //     List<Transform> children = new();
        //     foreach (Transform child in root.GetComponentsInChildren<Transform>())
        //         if (child.name == name && child != root)
        //             children.Add(child);
        //     return children.ToArray();
        // }
        // public static Transform[] GetAllChildren(this GameObject root, string name) => root.transform.GetAllChildren(name);

        // Zusammenfassung:
        //     Finds a child by name n and returns it.
        //
        // Parameter:
        //   n:
        //     Name of child to be found.
        //
        // Rückgabewerte:
        //     The found child transform. Null if child with matching name isn't found.
        // public static GameObject Find(this GameObject obj, string n) => obj.transform.Find(n).gameObject;

        //public static Transform[] GetChildren(this Transform root) {
        //    if(root == null)
        //        return null;
        //    if(root.childCount == 0)
        //        return null;
        //    Transform[] children = new Transform[root.childCount];
        //    for(int i = 0; i < root.childCount; i++)
        //        children[i] = root.GetChild(i);
        //    return children;
        //}

        //public static Transform[] GetChildren(this Transform root, string name) {
        //    if(root == null)
        //        return null;
        //    if(root.childCount == 0)
        //        return null;
        //    List<Transform> children = new();
        //    Transform child;
        //    for(int i = 0; i < root.childCount; i++) {
        //        child = root.GetChild(i);
        //        if(child.name == name)
        //            children.Add(child);
        //    }
        //    return children.ToArray();
        //}

        //public static Transform[] GetChildren(this GameObject root) => root.transform.GetChildren();
        //public static Transform[] GetChildren(this GameObject root, string name) => root.transform.GetChildren(name);

        // public static void LoadAssetPreview(this Image image, GameObject prefab, int size = 128)
        // {
        //     if (AssetPreviewProvider.TryGetSprite(prefab.GetInstanceID(), out Sprite sprite))
        //     {
        //         image.sprite = sprite;
        //     }
        //     else
        //     {
        //         AssetPreview.GetAssetPreview(prefab);
        //         AssetPreviewProvider.ImageLoadQueue.Add(new(prefab.GetInstanceID(), image, prefab));
        //     }
        // }

        public static int Abs(this int value) => Mathf.Abs(value);

        public static float Abs(this float value) => Mathf.Abs(value);

        public static char ToChar(this bool value) => value ? 'T' : 'F';

        // public static float HorizontalDistance(this Vector3 posA, Vector3 posB) =>
        //     Vector3.Distance(new Vector3(posA.x, 0, posA.z), new Vector3(posB.x, 0, posB.z));

        public static Vector3 ClosestAxisDirection(this Vector3 vector)
        {
            switch (vector.MaxAxis())
            {
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

        public static Axis MaxAxis(this Vector3 vector)
        {
            Vector3 abs = vector.Abs();
            float max = Math.Max(abs.X, abs.Y, abs.Z);
            if (max == abs.X)
                return Axis.x;
            if (max == abs.Y)
                return Axis.y;
            return Axis.z;
        }

        public static Vector3 Abs(this Vector3 vector) => new(vector.X.Abs(), vector.Y.Abs(), vector.Z.Abs());

        // public static Vector3 AxisDirection(this Transform transform, Axis axis)
        // {
        //     switch (axis)
        //     {
        //         case Axis.x:
        //             return transform.right;
        //         case Axis.y:
        //             return transform.up;
        //         case Axis.z:
        //             return transform.forward;
        //         default:
        //             return Vector3.Zero;
        //     }
        // }

        // /// <summary>
        // /// Returns the delta vector between the transform and the position, from the perspective of the transform.
        // /// </summary>
        // /// <param name="transform">Origin</param>
        // /// <param name="position">Target</param>
        // /// <returns>Delta Vector</returns>
        // public static Vector3 TransformDelta(this Transform transform, Vector3 position) =>
        //     transform.InverseTransformDirection(position - transform.position);
    }

    public static class Math
    {
        public static float Min(params float[] args){
            float min = float.MaxValue;
            foreach (float value in args){
                if(value < min)
                    min = value;
            }
            return min;
        }

        public static float Max(params float[] args){
            float max = float.MinValue;
            foreach (float value in args){
                if(value > max)
                    max = value;
            }
            return max;
        }

        public static float Confine(float value, float min, float max) => Max(Min(value, max), min);

        public static int Random(int min, int max, int seed) => 
            new RandomNumberGenerator(){Seed = (ulong)seed}.RandiRange(min, max);

        // public static Vector2[] Intersect2Circles(Vector2 centerA, float radiusA, Vector2 centerB, float radiusB)
        // {
        //     // A, B = [ x, y ]
        //     // return = [ Q1, Q2 ] or [ Q ] or [] where Q = [ x, y ]
        //     Vector2[] result = new Vector2[2];
        //     float AB0 = centerB.x - centerA.x;
        //     float AB1 = centerB.y - centerA.y;
        //     float c = Vector2.Distance(centerA, centerB);
        //     if (c == 0) // no distance between centers
        //         return result;
        //     float x = (radiusA * radiusA + c * c - radiusB * radiusB) / (2 * c);
        //     float y = radiusA * radiusA - x * x;
        //     if (y < 0) // no intersection
        //         return result;
        //     if (y > 0)
        //         y = Mathf.Sqrt(y);
        //     // compute unit vectors ex and ey
        //     float ex0 = AB0 / c;
        //     float ex1 = AB1 / c;
        //     float ey0 = -ex1;
        //     float ey1 = ex0;
        //     float Q1x = centerA.x + x * ex0;
        //     float Q1y = centerA.y + x * ex1;
        //     if (y == 0)
        //     {
        //         // one touch point
        //         result[0] = new Vector2(Q1x, Q1y);
        //         return result;
        //     }
        //     // two intersections
        //     float Q2x = Q1x - y * ey0;
        //     float Q2y = Q1y - y * ey1;
        //     Q1x += y * ey0;
        //     Q1y += y * ey1;
        //     result[0] = new Vector2(Q1x, Q1y);
        //     result[1] = new Vector2(Q2x, Q2y);
        //     return result;
        // }

        // public static bool CylinderCollision(Vector3 posA, float radiusA, float heightA, Axis axisA,
        //                                      Vector3 posB, float radiusB, float heightB, Axis axisB)
        // {
        //     Vector3 delta = posB - posA;
        //     int a, b, c;
        //     if (axisA == axisB)
        //     {
        //         a = (int)axisA;
        //         b = (a + 1) % 3;
        //         c = (a + 2) % 3;
        //         if ((heightA + heightB) / 2 < Mathf.Abs(delta[a]))
        //             return false;
        //         if (radiusA + radiusB < Mathf.Sqrt(delta[b] * delta[b] + delta[c] * delta[c]))
        //             return false;
        //     }
        //     else
        //     {
        //         a = (int)axisA;
        //         b = (int)axisB;
        //         c = 3 - a - b;

        //         posA[a] += posB[a] > posA[a] ? heightA / 2 : -heightA / 2;
        //         posB[b] += posA[b] > posB[b] ? heightB / 2 : -heightB / 2;

        //         if (Mathf.Abs(posA[b] - posB[b]) > radiusA || Mathf.Abs(posA[a] - posB[a]) > radiusB)
        //             return false;

        //         float diffA = Mathf.Sqrt(radiusA * radiusA - delta[b] * delta[b]);
        //         float diffB = Mathf.Sqrt(radiusB * radiusB - delta[a] * delta[a]);

        //         float cA1 = posA[c] + diffA;
        //         float cA2 = posA[c] - diffA;
        //         float cB1 = posB[c] + diffB;
        //         float cB2 = posB[c] - diffB;

        //         Vector3 LA1, LA2, LB1, LB2;
        //         LA1 = LA2 = LB1 = LB2 = Vector3.zero;
        //         LA1[a] = LA2[a] = LB1[a] = LB2[a] = posA[a];
        //         LA1[b] = LA2[b] = LB1[b] = LB2[b] = posB[b];

        //         LA1[c] = cA1;
        //         LA2[c] = cA2;
        //         LB1[c] = cB1;
        //         LB2[c] = cB2;

        //         if ((posA - LB1).magnitude > radiusA &&
        //            (posA - LB2).magnitude > radiusA &&
        //            (posB - LA1).magnitude > radiusB &&
        //            (posB - LA2).magnitude > radiusB)
        //             return false;
        //     }
        //     return true;
        // }
    }
}
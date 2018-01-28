using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static readonly float epsilon = 0.001f;

    public static bool Approximate(float a, float b)
    {
        return Mathf.Abs(a - b) < epsilon;
    }

    public static bool Approximate(float a, float b, float epsilon)
    {
        return Mathf.Abs(a - b) < epsilon;
    }

    public static bool Approximate(Vector2 a, Vector2 b)
    {
        return Approximate(a.x, b.x) && Approximate(a.y, b.y);
    }

    public static bool Approximate(Vector3 a, Vector3 b)
    {
        return Approximate(a.x, b.x) && Approximate(a.y, b.y) && Approximate(a.z, b.z);
    }

    public static bool Approximate(Vector3 a, Vector3 b, float epsilon)
    {
        return Approximate(a.x, b.x, epsilon) && Approximate(a.y, b.y, epsilon) && Approximate(a.z, b.z, epsilon);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {
    /// <summary>
    /// Rotates this vector around the given degree.
    /// </summary>
    /// <param name="v"></param>
    /// <param name="degrees"></param>
    /// <returns>The rotated vector</returns>
    public static Vector2 Rotate(this Vector2 v, float degrees) {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;

        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);

        return v;
    }
}

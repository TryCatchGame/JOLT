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

    public static T[] AddElement<T>(this T[] array, T newValue) {
        int newLength = array.Length + 1;

        T[] result = new T[newLength];

        for (int i = 0; i < array.Length; ++i) {
            result[i] = array[i];
        }

        result[newLength - 1] = newValue;

        return result;
    }
}

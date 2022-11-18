using UnityEngine;


public static class Vector3Extension
{
    public static Vector2 To2D(this Vector3 v3) => new Vector2(v3.x, v3.z);
}
using UnityEngine;

namespace Extension
{
    public static class Vector2Extension
    {
        public static Vector3 To3D(this Vector2 v2, float height = 0) => new Vector3(v2.x, height, v2.y);
    }
}
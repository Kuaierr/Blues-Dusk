using UnityEngine;

namespace GameKit
{
    public static partial class UnityExtension
    {
        public static Vector2 ToLocal(this Vector2 position, Transform transform)
        {
            return transform.InverseTransformPoint(position);
        }

        public static Vector3 ToLocal(this Vector3 position, Transform transform)
        {
            return transform.InverseTransformPoint(position);
        }
    }
}

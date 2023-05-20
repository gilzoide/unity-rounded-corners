using UnityEngine;

namespace Gilzoide.RoundedCorners
{
    public static class MathExtensions
    {
        #region Vector2

        public static Vector2 Rotated(this Vector2 v, float angle)
        {
            angle = Mathf.Deg2Rad * angle;
            var cos = Mathf.Cos(angle);
            var sin = Mathf.Sin(angle);
            return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
        }

        #endregion

        #region Rect

        public static Vector2 GetBottomLeft(this Rect rect)
        {
            return new Vector2(rect.xMin, rect.yMin);
        }

        public static Vector2 GetTopLeft(this Rect rect)
        {
            return new Vector2(rect.xMin, rect.yMax);
        }

        public static Vector2 GetTopRight(this Rect rect)
        {
            return new Vector2(rect.xMax, rect.yMax);
        }

        public static Vector2 GetBottomRight(this Rect rect)
        {
            return new Vector2(rect.xMax, rect.yMin);
        }

        public static Rect Inset(this Rect rect, float value)
        {
            return Rect.MinMaxRect(rect.xMin + value, rect.yMin + value, rect.xMax - value, rect.yMax - value);
        }

        public static bool HasArea(this Rect rect)
        {
            return rect.width > 0 && rect.height > 0;
        }

        #endregion
    }
}

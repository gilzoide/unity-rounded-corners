using System;
using System.Collections.Generic;
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

        public static Vector2 GetCornerPoint(this Rect rect, RectCorner corner)
        {
            switch (corner)
            {
                case RectCorner.BottomLeft: return rect.GetBottomLeft();
                case RectCorner.TopLeft: return rect.GetTopLeft();
                case RectCorner.TopRight: return rect.GetTopRight();
                case RectCorner.BottomRight: return rect.GetBottomRight();
                default: throw new ArgumentOutOfRangeException(nameof(corner));
            }
        }

        public static Rect Inset(this Rect rect, float value)
        {
            return Rect.MinMaxRect(rect.xMin + value, rect.yMin + value, rect.xMax - value, rect.yMax - value);
        }

        public static bool HasArea(this Rect rect)
        {
            return rect.width > 0 && rect.height > 0;
        }

        public static IEnumerable<Vector2> EnumerateCornerPoints(this Rect rect, RectCorner corner, RoundedCorner roundParameters, float maxRadius)
        {
            float radius = Mathf.Min(maxRadius, roundParameters.Radius);
            if (radius <= 0)
            {
                yield return rect.GetCornerPoint(corner);
                yield break;
            }

            Vector2 pivotPoint = rect.Inset(radius).GetCornerPoint(corner);
            Vector2 direction = new Vector2(radius, 0);
            (float startAngle, float endAngle) = corner.GetAngleRangeFromCenter();
            yield return pivotPoint + direction.Rotated(startAngle);
            
            int count = Mathf.Min(Mathf.FloorToInt(radius), roundParameters.TriangleCount);
            for (int i = 0; i < count; i++)
            {
                yield return pivotPoint + direction.Rotated(Mathf.Lerp(startAngle, endAngle, (float) (i + 1) / (float) count));
            }
        }

        #endregion
    }
}

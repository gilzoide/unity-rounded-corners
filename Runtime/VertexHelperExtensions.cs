using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gilzoide.RoundedCorners
{
    public static class VertexHelperExtensions
    {
        public static void AddRoundedRect(this VertexHelper vh, MeshType meshType, Rect rect, float cornerRadius, int cornerTriangleCount, IVertexColorProvider colorProvider = null, IVertexUvProvider uvProvider = null)
        {
            AddRoundedRect(vh, meshType, rect, new RoundedCorner { Radius = cornerRadius, TriangleCount = cornerTriangleCount }, colorProvider, uvProvider);
        }
    
        public static void AddRoundedRect(this VertexHelper vh, MeshType meshType, Rect rect, RoundedCorner cornerConfig, IVertexColorProvider colorProvider = null, IVertexUvProvider uvProvider = null)
        {
            AddRoundedRect(vh, meshType, rect, cornerConfig, cornerConfig, cornerConfig, cornerConfig, colorProvider, uvProvider);
        }

        public static void AddRoundedRect(this VertexHelper vh, MeshType meshType, Rect rect, RoundedCorner bottomLeft, RoundedCorner topLeft, RoundedCorner topRight, RoundedCorner bottomRight, IVertexColorProvider colorProvider = null, IVertexUvProvider uvProvider = null)
        {
            switch (meshType)
            {
                case MeshType.Inside:
                    vh.AddInsideRoundedRect(rect, bottomLeft, topLeft, topRight, bottomRight, colorProvider, uvProvider);
                    break;
                    
                case MeshType.Outside:
                    vh.AddOutsideRoundedRect(rect, bottomLeft, topLeft, topRight, bottomRight, colorProvider, uvProvider);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(meshType));
            }
        }

        #region Inside Rounded Rect

        public static void AddInsideRoundedRect(this VertexHelper vh, Rect rect, float cornerRadius, int cornerTriangleCount, IVertexColorProvider colorProvider = null, IVertexUvProvider uvProvider = null)
        {
            AddInsideRoundedRect(vh, rect, new RoundedCorner { Radius = cornerRadius, TriangleCount = cornerTriangleCount }, colorProvider, uvProvider);
        }
    
        public static void AddInsideRoundedRect(this VertexHelper vh, Rect rect, RoundedCorner cornerConfig, IVertexColorProvider colorProvider = null, IVertexUvProvider uvProvider = null)
        {
            AddInsideRoundedRect(vh, rect, cornerConfig, cornerConfig, cornerConfig, cornerConfig, colorProvider, uvProvider);
        }

        public static void AddInsideRoundedRect(this VertexHelper vh, Rect rect, RoundedCorner bottomLeft, RoundedCorner topLeft, RoundedCorner topRight, RoundedCorner bottomRight, IVertexColorProvider colorProvider = null, IVertexUvProvider uvProvider = null)
        {
            float maxRadius = GetMaxRadius(rect);

            int centerVertexIndex = vh.currentIndexCount;
            PopulatePoint(vh, rect, rect.center, colorProvider, uvProvider);

            int currentVertexIndex = vh.currentVertCount;
            foreach(Vector2 v in rect.EnumerateCornerPoints(RectCorner.BottomLeft, bottomLeft, maxRadius))
            {
                PopulatePoint(vh, rect, v, colorProvider, uvProvider);
                currentVertexIndex++;
                if (currentVertexIndex >= 3)
                {
                    vh.AddTriangle(centerVertexIndex, currentVertexIndex - 2, currentVertexIndex - 1);
                }
            }
            foreach(Vector2 v in rect.EnumerateCornerPoints(RectCorner.TopLeft, topLeft, maxRadius))
            {
                PopulatePoint(vh, rect, v, colorProvider, uvProvider);
                currentVertexIndex++;
                vh.AddTriangle(centerVertexIndex, currentVertexIndex - 2, currentVertexIndex - 1);
            }
            foreach(Vector2 v in rect.EnumerateCornerPoints(RectCorner.TopRight, topRight, maxRadius))
            {
                PopulatePoint(vh, rect, v, colorProvider, uvProvider);
                currentVertexIndex++;
                vh.AddTriangle(centerVertexIndex, currentVertexIndex - 2, currentVertexIndex - 1);
            }
            foreach(Vector2 v in rect.EnumerateCornerPoints(RectCorner.BottomRight, bottomRight, maxRadius))
            {
                PopulatePoint(vh, rect, v, colorProvider, uvProvider);
                currentVertexIndex++;
                vh.AddTriangle(centerVertexIndex, currentVertexIndex - 2, currentVertexIndex - 1);
            }
            vh.AddTriangle(centerVertexIndex, currentVertexIndex - 1, 1);
        }

        #endregion

        #region Outside Rounded Rect

        public static void AddOutsideRoundedRect(this VertexHelper vh, Rect rect, float cornerRadius, int cornerTriangleCount, IVertexColorProvider colorProvider = null, IVertexUvProvider uvProvider = null)
        {
            AddOutsideRoundedRect(vh, rect, new RoundedCorner { Radius = cornerRadius, TriangleCount = cornerTriangleCount }, colorProvider, uvProvider);
        }
    
        public static void AddOutsideRoundedRect(this VertexHelper vh, Rect rect, RoundedCorner cornerConfig, IVertexColorProvider colorProvider = null, IVertexUvProvider uvProvider = null)
        {
            AddOutsideRoundedRect(vh, rect, cornerConfig, cornerConfig, cornerConfig, cornerConfig, colorProvider, uvProvider);
        }

        public static void AddOutsideRoundedRect(this VertexHelper vh, Rect rect, RoundedCorner bottomLeft, RoundedCorner topLeft, RoundedCorner topRight, RoundedCorner bottomRight, IVertexColorProvider colorProvider = null, IVertexUvProvider uvProvider = null)
        {
            float maxRadius = GetMaxRadius(rect);
            PopulateOutsideCorner(vh, rect, RectCorner.BottomLeft, bottomLeft, maxRadius, colorProvider, uvProvider);
            PopulateOutsideCorner(vh, rect, RectCorner.TopLeft, topLeft, maxRadius, colorProvider, uvProvider);
            PopulateOutsideCorner(vh, rect, RectCorner.TopRight, topRight, maxRadius, colorProvider, uvProvider);
            PopulateOutsideCorner(vh, rect, RectCorner.BottomRight, bottomRight, maxRadius, colorProvider, uvProvider);
        }

        #endregion

        private static float GetMaxRadius(Rect rect)
        {
            float maxRadius = Mathf.Min(rect.width, rect.height) * 0.5f;
            return maxRadius;
        }

        private static void PopulatePoint(VertexHelper vh, Rect fullRect, Vector2 point, IVertexColorProvider colorProvider, IVertexUvProvider uvProvider)
        {
            Vector2 normalizedPosition = Rect.PointToNormalized(fullRect, point);
            Color color = colorProvider?.GetVertexColor(normalizedPosition) ?? Color.white;
            Vector2 uv = uvProvider?.GetVertexUv(normalizedPosition) ?? normalizedPosition;
            vh.AddVert(point, color, uv);
        }

        private static void PopulateOutsideCorner(VertexHelper vh, Rect rect, RectCorner corner, RoundedCorner roundedCorner, float maxRadius, IVertexColorProvider colorProvider = null, IVertexUvProvider uvProvider = null)
        {
            if (roundedCorner.Radius <= 0)
            {
                return;
            }

            int cornerVertex = vh.currentVertCount;
            PopulatePoint(vh, rect, rect.GetCornerPoint(corner), colorProvider, uvProvider);

            int currentVertex = vh.currentVertCount;
            foreach (Vector2 point in rect.EnumerateCornerPoints(corner, roundedCorner, maxRadius))
            {
                PopulatePoint(vh, rect, point, colorProvider, uvProvider);
                if (currentVertex - cornerVertex >= 2)
                {
                    vh.AddTriangle(cornerVertex, currentVertex, currentVertex - 1);
                }
                currentVertex++;
            }
        }
    }
}

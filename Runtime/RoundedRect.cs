using UnityEngine;
using UnityEngine.UI;

namespace Gilzoide.RoundedCorners
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class RoundedRect : MaskableGraphic
    {
        [Header("Corner Radius")]
        [SerializeField] protected RoundedCorner _bottomLeft = new RoundedCorner { Radius = 32, TriangleCount = 8 };
        [SerializeField] protected RoundedCorner _topLeft = new RoundedCorner { Radius = 32, TriangleCount = 8 };
        [SerializeField] protected RoundedCorner _topRight = new RoundedCorner { Radius = 32, TriangleCount = 8 };
        [SerializeField] protected RoundedCorner _bottomRight = new RoundedCorner { Radius = 32, TriangleCount = 8 };

        /// <summary>Radius of the bottom left corner.</summary>
        public RoundedCorner BottomLeft
        {
            get => _bottomLeft;
            set
            {
                _bottomLeft = value;
                SetVerticesDirty();
            }
        }

        /// <summary>Radius of the top left corner.</summary>
        public RoundedCorner TopLeft
        {
            get => _topLeft;
            set
            {
                _topLeft = value;
                SetVerticesDirty();
            }
        }

        /// <summary>Radius of the top right corner.</summary>
        public RoundedCorner TopRight
        {
            get => _topRight;
            set
            {
                _topRight = value;
                SetVerticesDirty();
            }
        }

        /// <summary>Radius of the bottom right corner.</summary>
        public RoundedCorner BottomRight
        {
            get => _bottomRight;
            set
            {
                _bottomRight = value;
                SetVerticesDirty();
            }
        }

        #region Mesh generation

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            Rect rect = rectTransform.rect;
            if (!rect.HasArea())
            {
                return;
            }

            PopulatePoint(vh, rect, rect.center);
            int vertexCount = 1;

            float maxRadius = Mathf.Min(rect.width, rect.height) * 0.5f;
            foreach(Vector2 v in rect.EnumerateCornerPoints(RectCorner.BottomLeft, BottomLeft, maxRadius))
            {
                PopulatePoint(vh, rect, v);
                vertexCount++;
                if (vertexCount >= 3)
                {
                    vh.AddTriangle(0, vertexCount - 2, vertexCount - 1);
                }
            }
            foreach(Vector2 v in rect.EnumerateCornerPoints(RectCorner.TopLeft, TopLeft, maxRadius))
            {
                PopulatePoint(vh, rect, v);
                vertexCount++;
                vh.AddTriangle(0, vertexCount - 2, vertexCount - 1);
            }
            foreach(Vector2 v in rect.EnumerateCornerPoints(RectCorner.TopRight, TopRight, maxRadius))
            {
                PopulatePoint(vh, rect, v);
                vertexCount++;
                vh.AddTriangle(0, vertexCount - 2, vertexCount - 1);
            }
            foreach(Vector2 v in rect.EnumerateCornerPoints(RectCorner.BottomRight, BottomRight, maxRadius))
            {
                PopulatePoint(vh, rect, v);
                vertexCount++;
                vh.AddTriangle(0, vertexCount - 2, vertexCount - 1);
            }
            vh.AddTriangle(0, vertexCount - 1, 1);
        }

        protected void PopulatePoint(VertexHelper vh, Rect fullRect, Vector2 point)
        {
            Vector2 uv = GetUVForNormalizedPosition(Rect.PointToNormalized(fullRect, point));
            vh.AddVert(point, color, uv);
        }

        protected virtual Vector2 GetUVForNormalizedPosition(Vector2 position)
        {
            return position;
        }

        #endregion
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace Gilzoide.RoundedCorners
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class RoundedRect : MaskableGraphic
    {
        [Header("Corners")]
        [Tooltip("Corner radius, in Canvas units.")]
        [SerializeField, Min(0)] protected float _radius = 32;

        [Tooltip("Number of triangles generated per rounded corner. In particular, setting this to 1 makes corners flat, as in an octogon.")]
        [SerializeField, Min(1)] protected int _cornerTriangles = 8;

        [Space]
        [Tooltip("Whether the bottom left corner should be rounded.")]
        [SerializeField] protected bool _bottomLeft = true;

        [Tooltip("Whether the top left corner should be rounded.")]
        [SerializeField] protected bool _topLeft = true;

        [Tooltip("Whether the top right corner should be rounded.")]
        [SerializeField] protected bool _topRight = true;

        [Tooltip("Whether the bottom right corner should be rounded.")]
        [SerializeField] protected bool _bottomRight = true;

        /// <summary>Corner radius, in Canvas units.</summary>
        public float Radius
        {
            get => _radius;
            set
            {
                value = Mathf.Max(0, value);
                if (_radius != value)
                {
                    _radius = value;
                    SetVerticesDirty();
                }
            }
        }

        /// <summary>Number of triangles generated per rounded corner.</summary>
        /// <remarks>In particular, setting this to 1 makes corners flat, as in an octogon.</remarks>
        public int CornerTriangles
        {
            get => _cornerTriangles;
            set
            {
                value = Mathf.Max(1, value);
                if (_cornerTriangles != value)
                {
                    _cornerTriangles = value;
                    SetVerticesDirty();
                }
            }
        }

        /// <summary>Whether the bottom left corner should be rounded.</summary>
        public bool BottomLeft
        {
            get => _bottomLeft;
            set
            {
                _bottomLeft = value;
                SetVerticesDirty();
            }
        }

        /// <summary>Whether the top left corner should be rounded.</summary>
        public bool TopLeft
        {
            get => _topLeft;
            set
            {
                _topLeft = value;
                SetVerticesDirty();
            }
        }

        /// <summary>Whether the top right corner should be rounded.</summary>
        public bool TopRight
        {
            get => _topRight;
            set
            {
                _topRight = value;
                SetVerticesDirty();
            }
        }

        /// <summary>Whether the bottom right corner should be rounded.</summary>
        public bool BottomRight
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

            Rect fullRect = rectTransform.rect;
            float radius = Mathf.Clamp(_radius, 0, Mathf.Min(fullRect.width, fullRect.height) * 0.5f);
            if (radius > 0)
            {
                Rect centerRect = fullRect.Inset(radius);
                if (_bottomLeft)
                {
                    PopulateCorner(vh, fullRect, centerRect.GetBottomLeft(), radius, -90, -180);
                }
                else
                {
                    PopulateQuad(vh, fullRect, Rect.MinMaxRect(fullRect.xMin, fullRect.yMin, centerRect.xMin, centerRect.yMin));
                }

                if (_topLeft)
                {
                    PopulateCorner(vh, fullRect, centerRect.GetTopLeft(), radius, 180, 90);
                }
                else
                {
                    PopulateQuad(vh, fullRect, Rect.MinMaxRect(fullRect.xMin, centerRect.yMax, centerRect.xMin, fullRect.yMax));
                }

                if (_topRight)
                {
                    PopulateCorner(vh, fullRect, centerRect.GetTopRight(), radius, 90, 0);
                }
                else
                {
                    PopulateQuad(vh, fullRect, Rect.MinMaxRect(centerRect.xMax, centerRect.yMax, fullRect.xMax, fullRect.yMax));
                }

                if (_bottomRight)
                {
                    PopulateCorner(vh, fullRect, centerRect.GetBottomRight(), radius, 0, -90);
                }
                else
                {
                    PopulateQuad(vh, fullRect, Rect.MinMaxRect(centerRect.xMax, fullRect.yMin, fullRect.xMax, centerRect.yMin));
                }

                PopulateQuad(vh, fullRect, Rect.MinMaxRect(fullRect.xMin, centerRect.yMin, centerRect.xMin, centerRect.yMax));
                PopulateQuad(vh, fullRect, Rect.MinMaxRect(centerRect.xMax, centerRect.yMin, fullRect.xMax, centerRect.yMax));
                PopulateQuad(vh, fullRect, Rect.MinMaxRect(centerRect.xMin, fullRect.yMin, centerRect.xMax, fullRect.yMax));
            }
            else
            {
                PopulateQuad(vh, fullRect, fullRect);
            }
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

        protected void PopulateQuad(VertexHelper vh, Rect fullRect, Rect rect)
        {
            if (!rect.HasArea())
            {
                return;
            }

            int vertexCount = vh.currentVertCount;

            PopulatePoint(vh, fullRect, rect.GetBottomLeft());
            PopulatePoint(vh, fullRect, rect.GetTopLeft());
            PopulatePoint(vh, fullRect, rect.GetTopRight());
            PopulatePoint(vh, fullRect, rect.GetBottomRight());

            vh.AddTriangle(vertexCount, vertexCount + 1, vertexCount + 2);
            vh.AddTriangle(vertexCount + 2, vertexCount + 3, vertexCount);
        }

        protected void PopulateCorner(VertexHelper vh, Rect fullRect, Vector2 point, float radius, float startAngle, float endAngle)
        {
            Vector2 direction = new Vector2(radius, 0);
            int vertexCount = vh.currentVertCount;

            PopulatePoint(vh, fullRect, point);
            PopulatePoint(vh, fullRect, point + direction.Rotated(startAngle));

            int count = Mathf.Min(Mathf.FloorToInt(radius), _cornerTriangles);
            for (int i = 0; i < count; i++)
            {
                PopulatePoint(vh, fullRect, point + direction.Rotated(Mathf.Lerp(startAngle, endAngle, (float) (i + 1) / (float) count)));
                vh.AddTriangle(vertexCount, vertexCount + i + 1, vertexCount + i + 2);
            }
        }

        #endregion
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace Gilzoide.RoundedCorners
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class RoundedRect : MaskableGraphic, IVertexColorProvider, IVertexUvProvider
    {
        [Header("Corner Radius")]
        [SerializeField] protected RoundedCorner _bottomLeft = new RoundedCorner { Radius = 32, TriangleCount = 8 };
        [SerializeField] protected RoundedCorner _topLeft = new RoundedCorner { Radius = 32, TriangleCount = 8 };
        [SerializeField] protected RoundedCorner _topRight = new RoundedCorner { Radius = 32, TriangleCount = 8 };
        [SerializeField] protected RoundedCorner _bottomRight = new RoundedCorner { Radius = 32, TriangleCount = 8 };
        
        [Tooltip("Whether to render the inside or outside of the rounded rectangle.")]
        [SerializeField] protected MeshType _meshType = MeshType.Inside;

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

        /// <summary>Whether to render the inside or outside of the rounded rectangle.</summary>
        public MeshType MeshType
        {
            get => _meshType;
            set
            {
                _meshType = value;
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

            vh.AddRoundedRect(_meshType, rect, BottomLeft, TopLeft, TopRight, BottomRight, this, this);  
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

        #region IVertexColorProvider

        public Color GetVertexColor(Vector2 normalizedRectPosition)
        {
            return color;
        }

        #endregion

        #region IVertexUvProvider

        public Vector2 GetVertexUv(Vector2 normalizedRectPosition)
        {
            return GetUVForNormalizedPosition(normalizedRectPosition);
        }

        #endregion
    }
}

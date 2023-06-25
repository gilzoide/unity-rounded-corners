using System;

namespace Gilzoide.RoundedCorners
{
    public enum RectCorner
    {
        BottomLeft,
        TopLeft,
        TopRight,
        BottomRight,
    }

    public static class RectCornerExtensions
    {
        public static (float, float) GetAngleRangeFromCenter(this RectCorner corner)
        {
            switch (corner)
            {
                case RectCorner.BottomLeft: return (-90, -180);
                case RectCorner.TopLeft: return (180, 90);
                case RectCorner.TopRight: return (90, 0);
                case RectCorner.BottomRight: return (0, -90);
                default: throw new ArgumentOutOfRangeException(nameof(corner));
            }
        }
    }
}

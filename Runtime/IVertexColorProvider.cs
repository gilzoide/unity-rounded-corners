using UnityEngine;

namespace Gilzoide.RoundedCorners
{
    public interface IVertexColorProvider
    {
        Color GetVertexColor(Vector2 normalizedRectPosition);
    }
}

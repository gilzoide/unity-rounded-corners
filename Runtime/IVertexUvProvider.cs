using UnityEngine;

namespace Gilzoide.RoundedCorners
{
    public interface IVertexUvProvider
    {
        Vector2 GetVertexUv(Vector2 normalizedRectPosition);
    }
}

using System;
using UnityEngine;

namespace Gilzoide.RoundedCorners
{
    [Serializable]
    public struct RoundedCorner
    {
        [Tooltip("Corner radius, in canvas units. If set to 0 the corner will not be rounded.")]
        [Min(0)] public float Radius;
        
        [Tooltip("Number of triangles generated. In particular, setting this to 1 makes corners flat, as in an octogon.")]
        [Min(1)] public int TriangleCount;
    }
}

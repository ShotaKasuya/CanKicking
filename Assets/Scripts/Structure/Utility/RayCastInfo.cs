using UnityEngine;

namespace Structure.Utility
{
    public ref struct RayCastInfo
    {
        public Vector2 Direction { get; }
        public float Distance { get; }
        public int LayerMask { get; }

        public RayCastInfo
        (
            Vector2 direction,
            float distance,
            int layerMask
        )
        {
            Direction = direction;
            Distance = distance;
            LayerMask = layerMask;
        }
    }
}
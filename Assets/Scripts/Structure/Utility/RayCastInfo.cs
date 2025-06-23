using System;
using UnityEngine;

namespace Structure.Util
{
    [Serializable]
    public ref struct RayCastInfo
    {
        public Vector2 Direction => direction;
        public float Distance => distance;
        public int LayerMask => layerMask;

        [SerializeField] private Vector2 direction;
        [SerializeField] private float distance;
        [SerializeField] private int layerMask;
    }
}
using System;
using UnityEngine;

namespace Module.FadeContainer.Runtime
{
    [Serializable]
    public class FadeEntity
    {
        public Transform Target => fadeTarget;
        public Vector3 FadeInPosition => fadeInPosition.position;
        public Vector3 FadeOutPosition => fadeOutPosition.position;

        [SerializeField] internal Transform fadeTarget;
        [SerializeField] internal Transform fadeInPosition;
        [SerializeField] internal Transform fadeOutPosition;
    }
}
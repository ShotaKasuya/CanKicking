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

        [SerializeField] private Transform fadeTarget;
        [SerializeField] private Transform fadeInPosition;
        [SerializeField] private Transform fadeOutPosition;
    }
}
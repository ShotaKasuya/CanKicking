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

        public Type GetTargetType()
        {
            if (string.IsNullOrEmpty(targetType))
            {
                return null;
            }

            return Type.GetType(targetType);
        }

        [SerializeField, HideInInspector] internal string targetType;
        [SerializeField] internal Transform fadeTarget;
        [SerializeField] internal Transform fadeInPosition;
        [SerializeField] internal Transform fadeOutPosition;
    }
}
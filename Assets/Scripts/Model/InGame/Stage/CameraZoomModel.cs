using System;
using Interface.InGame.Stage;
using UnityEngine;

namespace Model.InGame.Stage
{
    [Serializable]
    public class CameraZoomModel : ICameraZoomModel
    {
        public float GetOrthoSize()
        {
            var result = CalcOrtho();

            return result;
        }

        public float SetZoomLevel(float level)
        {
            level = Mathf.Clamp01(level);

            ZoomLevel = level;
            var result = CalcOrtho();

            return result;
        }

        private float CalcOrtho()
        {
            var result = zoomMaxLevel * (1 - ZoomLevel) + zoomMinLevel * ZoomLevel;

            return result;
        }

        public float ZoomLevel { get; private set; }
        public float Sensitivity => sensitivity;

        [SerializeField] private float sensitivity;
        [SerializeField] private float zoomMaxLevel;
        [SerializeField] private float zoomMinLevel;
    }
}
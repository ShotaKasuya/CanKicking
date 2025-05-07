using System.Runtime.CompilerServices;
using UnityEngine;

namespace DataUtil.Util.Input
{
    /// <summary>
    /// Unityへの依存を吸収する構造体
    /// </summary>
    public struct TouchState
    {
        public InputPhaseType PhaseType { get; }
        public Vector2 Position { get; }
        public Vector2 Delta { get; }
        public float Pressure { get; }
        public Vector2 Radius { get; }
        public byte TapCount { get; }
        public byte DisplayIndex { get; }
        public double StartTime { get; }
        public Vector2 StartPosition { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TouchState(
            UnityEngine.InputSystem.LowLevel.TouchState touchState
        )
        {
            PhaseType = touchState.phase.Conversion();
            Position = touchState.position;
            Delta = touchState.delta;
            Pressure = touchState.pressure;
            Radius = touchState.radius;
            TapCount = touchState.tapCount;
            DisplayIndex = touchState.displayIndex;
            StartTime = touchState.startTime;
            StartPosition = touchState.startPosition;
        }
    }
}
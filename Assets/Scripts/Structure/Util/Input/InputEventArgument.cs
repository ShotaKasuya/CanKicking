using System.Runtime.CompilerServices;
using UnityEngine;

namespace Structure.Util.Input
{
    public struct FingerTouchInfo
    {
        public Vector2 TouchPosition { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FingerTouchInfo(Vector2 touchPosition)
        {
            TouchPosition = touchPosition;
        }
    }
    
    public struct FingerDraggingInfo
    {
        public Vector2 TouchStartPosition { get; }
        public Vector2 TouchCurrentPosition { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FingerDraggingInfo(Vector2 touchStartPosition, Vector2 touchCurrentPosition)
        {
            TouchStartPosition = touchStartPosition;
            TouchCurrentPosition = touchCurrentPosition;
        }
    }

    public struct FingerReleaseInfo
    {
        public Vector2 TouchStartPosition { get; }
        public Vector2 TouchEndPosition { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FingerReleaseInfo(Vector2 touchStartPosition, Vector2 touchEndPosition)
        {
            TouchStartPosition = touchStartPosition;
            TouchEndPosition = touchEndPosition;
        }
    }
}
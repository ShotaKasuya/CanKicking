using DataUtil.Util.Input;
using UnityEngine;

namespace Adapter.IView.Finger
{
    public interface IFingerView
    {
        public InputPhaseType CursorPhaseType { get; }
        public Vector2 CursorPosition { get; }
        public Vector2 CurrentDelta { get; }
        public int TapCount { get; }
    }
}
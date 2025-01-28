using System;
using UnityEngine;

namespace Domain.IPresenter.Util.Input
{
    public interface IFingerTouchingEventPresenter
    {
        Action<FingerTouchingEventArg> TouchingEvent { get; set; }
    }

    [Serializable]
    public struct FingerTouchingEventArg
    {
        public FingerTouchingEventArg
        (
            Vector2 startPosition,
            Vector2 currentTouchPosition,
            Vector2 delta
        )
        {
            StartPosition = startPosition;
            CurrentTouchPosition = currentTouchPosition;
            Delta = delta;
        }

        public Vector2 StartPosition { get; }
        public Vector2 CurrentTouchPosition { get; }
        public Vector2 Delta { get; }
    }
}
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
            Vector2 currentTouchPosition
        )
        {
            StartPosition = startPosition;
            CurrentTouchPosition = currentTouchPosition;
        }

        public Vector2 StartPosition { get; }
        public Vector2 CurrentTouchPosition { get; }
    }
}
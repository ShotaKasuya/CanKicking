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
            Vector2 touchPosition
        )
        {
            TouchPosition = touchPosition;
        }

        public Vector2 TouchPosition { get; }
    }
}
using System;
using UnityEngine;

namespace Domain.IPresenter.Util.Input
{
    public interface IFingerTouchEventPresenter
    {
        Action<FingerTouchEventArg> TouchEvent { get; set; }
    }

    [Serializable]
    public struct FingerTouchEventArg
    {
        public FingerTouchEventArg
        (
            Vector2 touchPosition
        )
        {
            TouchPosition = touchPosition;
        }

        public Vector2 TouchPosition { get; }
    }
}
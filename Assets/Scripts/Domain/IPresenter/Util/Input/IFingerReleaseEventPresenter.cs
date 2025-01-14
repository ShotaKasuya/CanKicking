using System;
using UnityEngine;

namespace Domain.IPresenter.Util.Input
{
    public interface IFingerReleaseEventPresenter
    {
        Action<FingerReleaseEventArg> ReleaseEvent { get; set; }
    }
    public struct FingerReleaseEventArg
    {
        public FingerReleaseEventArg
        (
            Vector2 releasePosition,
            Vector2 fingerDelta
        )
        {
            ReleasePosition = releasePosition;
            FingerDelta = fingerDelta;
        }

        
        public Vector2 ReleasePosition { get; }
        public Vector2 FingerDelta { get; }
    }
}
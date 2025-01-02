using System;
using System.Runtime.CompilerServices;
using Adapter.IView.Finger;
using DataUtil.Util.Input;
using DataUtil.Util.Installer;
using Domain.IPresenter.Util.Input;
using UnityEngine;

namespace Adapter.Presenter.Util.Input
{
    public class FingerEventPresenter: IFingerReleaseEventPresenter, ITickable
    {
        public FingerEventPresenter
        (
            IFingerView fingerView
        )
        {
            FingerView = fingerView;
        }
        
        public Action<FingerReleaseEventArg> ReleaseEvent { get; set; }
        public void Tick(float deltaTime)
        {
            var phase = FingerView.CursorPhaseType;

            if (phase == _prevPhase)
            {
                return;
            }
            switch (phase)
            {
                case InputPhaseType.OnTouch:
                {
                    _onTouchPosition = FingerView.CursorPosition;
                    break;
                }
                case InputPhaseType.OnRelease:
                {
                    ReleaseEvent.Invoke(BuildReleaseEventArg());
                    break;
                }
            }

            _prevPhase = phase;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FingerReleaseEventArg BuildReleaseEventArg()
        {
            var cursorPosition = FingerView.CursorPosition;
            var delta = cursorPosition - _onTouchPosition;
            return new FingerReleaseEventArg(FingerView.CursorPosition, delta);
        }

        private Vector2 _onTouchPosition;
        private InputPhaseType _prevPhase = InputPhaseType.None;
        private IFingerView FingerView { get; }
    }
}
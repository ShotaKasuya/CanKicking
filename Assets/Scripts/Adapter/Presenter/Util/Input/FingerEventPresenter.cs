using System;
using System.Runtime.CompilerServices;
using Adapter.IView.Finger;
using DataUtil.Util.Input;
using DataUtil.Util.Installer;
using Domain.IPresenter.Util.Input;
using UnityEngine;

namespace Adapter.Presenter.Util.Input
{
    public class FingerEventPresenter: IFingerReleaseEventPresenter, IFingerTouchEventPresenter, IFingerTouchingEventPresenter, ITickable
    {
        public FingerEventPresenter
        (
            IFingerView fingerView
        )
        {
            FingerView = fingerView;
        }
        private InputPhaseType _currentPhase  = InputPhaseType.None;
        
        public Action<FingerTouchEventArg> TouchEvent { get; set; }
        public Action<FingerTouchingEventArg> TouchingEvent { get; set; }
        public Action<FingerReleaseEventArg> ReleaseEvent { get; set; }
        public void Tick(float deltaTime)
        {
            var newPhase = FingerView.CursorPhaseType;

            if (newPhase == _currentPhase)
            {
                return;
            }
            switch (newPhase)
            {
                case InputPhaseType.OnTouch:
                {
                    _onTouchPosition = FingerView.CursorPosition;
                    TouchEvent.Invoke(BuildTouchEventArg());
                    break;
                }
                case InputPhaseType.OnRelease:
                {
                    ReleaseEvent.Invoke(BuildReleaseEventArg());
                    break;
                }
                case InputPhaseType.Moving or InputPhaseType.Staying:
                {
                    TouchingEvent.Invoke(BuildTouchingEventArg());
                    break;
                }
            }

            _currentPhase = newPhase;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FingerTouchEventArg BuildTouchEventArg()
        {
            var touchPosition = _onTouchPosition;
            return new FingerTouchEventArg(touchPosition);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FingerTouchingEventArg BuildTouchingEventArg()
        {
            var cursorPosition = FingerView.CursorPosition;
            return new FingerTouchingEventArg(cursorPosition);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FingerReleaseEventArg BuildReleaseEventArg()
        {
            var cursorPosition = FingerView.CursorPosition;
            var delta = cursorPosition - _onTouchPosition;
            return new FingerReleaseEventArg(FingerView.CursorPosition, delta);
        }

        private Vector2 _onTouchPosition;
        private IFingerView FingerView { get; }
    }
}
using System;
using System.Runtime.CompilerServices;
using Adapter.IView.Finger;
using DataUtil.Util.Input;
using DataUtil.Util.Installer;
using Domain.IPresenter.Util.Input;

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
            var touchState = FingerView.TouchState;
            var newPhase = touchState.PhaseType;

            if (newPhase == _currentPhase && newPhase.IsEvent())
            {
                return;
            }
            switch (newPhase)
            {
                case InputPhaseType.OnTouch:
                {
                    TouchEvent.Invoke(BuildTouchEventArg(touchState));
                    break;
                }
                case InputPhaseType.OnRelease:
                {
                    ReleaseEvent.Invoke(BuildReleaseEventArg(touchState));
                    break;
                }
                case InputPhaseType.Moving or InputPhaseType.Staying:
                {
                    TouchingEvent?.Invoke(BuildTouchingEventArg(touchState));
                    break;
                }
            }

            _currentPhase = newPhase;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FingerTouchEventArg BuildTouchEventArg(TouchState touchState)
        {
            var touchPosition = touchState.StartPosition;
            return new FingerTouchEventArg(touchPosition);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FingerTouchingEventArg BuildTouchingEventArg(TouchState touchState)
        {
            var startPosition = touchState.StartPosition;
            var cursorPosition = touchState.Position;
            var delta = touchState.Delta;
            return new FingerTouchingEventArg(cursorPosition, startPosition, delta);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FingerReleaseEventArg BuildReleaseEventArg(TouchState touchState)
        {
            var cursorPosition = touchState.Position;
            var delta = cursorPosition - touchState.StartPosition;
            return new FingerReleaseEventArg(cursorPosition, delta);
        }

        private IFingerView FingerView { get; }
    }
}
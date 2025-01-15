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
            var newPhase = FingerView.TouchState.PhaseType;

            if (newPhase == _currentPhase && newPhase.IsEvent())
            {
                return;
            }
            switch (newPhase)
            {
                case InputPhaseType.OnTouch:
                {
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
                    TouchingEvent?.Invoke(BuildTouchingEventArg());
                    break;
                }
            }

            _currentPhase = newPhase;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FingerTouchEventArg BuildTouchEventArg()
        {
            var touchPosition = FingerView.TouchState.StartPosition;
            return new FingerTouchEventArg(touchPosition);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FingerTouchingEventArg BuildTouchingEventArg()
        {
            var touchState = FingerView.TouchState;
            var startPosition = touchState.StartPosition;
            var cursorPosition = touchState.Position;
            return new FingerTouchingEventArg(cursorPosition, startPosition);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FingerReleaseEventArg BuildReleaseEventArg()
        {
            var touchState = FingerView.TouchState;
            var cursorPosition = touchState.Position;
            var delta = cursorPosition - touchState.StartPosition;
            return new FingerReleaseEventArg(cursorPosition, delta);
        }

        private IFingerView FingerView { get; }
    }
}
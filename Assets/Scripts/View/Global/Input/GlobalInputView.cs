using System;
using Interface.Global.Input;
using Interface.InGame.Stage;
using VContainer.Unity;

namespace View.Global.Input
{
    public partial class GlobalInputView : ITouchView, IPinchView, IDoubleTapView, ITickable, IStartable, IDisposable
    {
        public GlobalInputView(InputSystem_Actions inputSystemActions)
        {
            Actions = inputSystemActions.Player;
        }

        public void Start()
        {
            Actions.Enable();
            Actions.Touch.performed += OnStartClick;
            Actions.Touch.canceled += OnReleaseInput;
            Actions.DoubleTap.performed += OnDoubleTap;
        }

        public void Tick()
        {
            TouchTick();
        }

        public void Dispose()
        {
            TouchSubject?.Dispose();
            TouchEndSubject?.Dispose();

            Actions.Touch.performed -= OnStartClick;
            Actions.Touch.canceled -= OnReleaseInput;
            Actions.Disable();
        }

        private InputSystem_Actions.PlayerActions Actions { get; }
    }
}
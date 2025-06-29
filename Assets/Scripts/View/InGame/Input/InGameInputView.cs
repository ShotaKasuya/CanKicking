using System;
using Interface.InGame.Player;
using Interface.InGame.Stage;
using VContainer.Unity;
using View.Global.Input;

namespace View.InGame.Input
{
    public partial class InGameInputView : ITouchView, IPinchView, IStartable, IDisposable
    {
        public InGameInputView
        (
            InputSystem_Actions inputSystemActions
        )
        {
            Actions = inputSystemActions.Player;
        }

        public void Start()
        {
            Actions.Enable();
            TouchInit();
        }

        public void Dispose()
        {
            TouchDispose();
            Actions.Disable();
        }

        private InputSystem_Actions.PlayerActions Actions { get; }
    }
}
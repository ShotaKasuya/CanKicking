using System;
using Interface.Global.Input;
using Interface.InGame.Stage;
using VContainer.Unity;

namespace View.Global.Input
{
    public partial class GlobalInputView : ITouchView, IPinchView, ITickable, IStartable, IDisposable
    {
        public GlobalInputView(InputSystem_Actions inputSystemActions)
        {
            Actions = inputSystemActions.Player;
        }

        public void Start()
        {
            Actions.Enable();
            TouchInit();
        }

        public void Tick()
        {
            TouchTick();
        }

        public void Dispose()
        {
            TouchDispose();
            Actions.Disable();
        }

        private InputSystem_Actions.PlayerActions Actions { get; }
    }
}
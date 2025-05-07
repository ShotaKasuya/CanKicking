using System;
using Adapter.IView.Finger;
using Adapter.View.InGame.Input;
using TouchState = UnityEngine.InputSystem.LowLevel.TouchState;

namespace Detail.View.InGame.Input
{
    /// <summary>
    /// 特例クラス
    /// インプットシステムのラッパ
    /// 生成コードにインターフェースをつけれないので
    /// </summary>
    public class InputView : IFingerView, IDisposable
    {
        public InputView()
        {
            var inputActions = new InputSystem_Actions();
            inputActions.Enable();

            PlayerInputActions = inputActions.Player;
            UIActions = inputActions.UI;
        }

        public void Tick(float deltaTime)
        {
            var input = ReadTouchState;
            TouchState = new DataUtil.Util.Input.TouchState(input);
        }

        public DataUtil.Util.Input.TouchState TouchState { get; private set; }

        private TouchState ReadTouchState => PlayerInputActions.Click.ReadValue<TouchState>();
        private InputSystem_Actions.PlayerActions PlayerInputActions { get; }
        private InputSystem_Actions.UIActions UIActions { get; }

        public void Dispose()
        {
            PlayerInputActions.Disable();
            UIActions.Disable();
        }
    }
}
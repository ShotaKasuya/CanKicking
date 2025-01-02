using Adapter.IView.Finger;
using DataUtil.Util;
using DataUtil.Util.Input;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace Detail.View.InGame.Input
{
    /// <summary>
    /// 特例クラス
    /// インプットシステムのラッパ
    /// 生成コードにインターフェースをつけれないので
    /// </summary>
    public class InputView : IFingerView
    {
        public InputView
            ()
        {
            var inputActions = new InputSystem_Actions();
            inputActions.Enable();

            PlayerInputActions = inputActions.Player;
        }

        public InputPhaseType CursorPhaseType => ReadTouchState.phase.Conversion();
        public Vector2 CursorPosition => ReadTouchState.position;
        public Vector2 CurrentDelta => ReadTouchState.delta;
        public int TapCount => ReadTouchState.tapCount;

        
        // todo: キャッシュしたほうがいいかも?
        private TouchState ReadTouchState => PlayerInputActions.Click.ReadValue<TouchState>();
        private InputSystem_Actions.PlayerActions PlayerInputActions { get; }
    }
}
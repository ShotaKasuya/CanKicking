using System;
using Adapter.IView.Finger;
using Adapter.View.Util;
using Module.Option;
using Structure.Util.Input;
using VContainer.Unity;
using TouchState = UnityEngine.InputSystem.LowLevel.TouchState;

namespace Adapter.View.InGame.Input
{
    /// <summary>
    /// 特例クラス
    /// `InputSystem`が生成するクラスのラッパ
    /// 生成コードにインターフェースをつけれないので
    /// </summary>
    public class InputView : ITickable, IFingerView, IDisposable
    {
        public InputView(InputSystem_Actions inputSystemActions)
        {
            PlayerInputActions = inputSystemActions.Player;
            PlayerInputActions.Enable();
        }

        public void Tick()
        {
            var touchInfo = new TouchInformation(PlayerInputActions.Click.ReadValue<TouchState>());

            if (DragInfo.IsNone & touchInfo.PhaseType == InputPhaseType.OnTouch)
            {
                OnTouch?.Invoke(new FingerTouchInfo(touchInfo.Position));
                DragInfo = Option<FingerDraggingInfo>.Some(new FingerDraggingInfo(
                    touchInfo.StartPosition,
                    touchInfo.StartPosition
                ));
                return;
            }

            if (DragInfo.TryGetValue(out var info))
            {
                if (touchInfo.PhaseType == InputPhaseType.OnRelease)
                {
                    DragInfo = Option<FingerDraggingInfo>.None();
                    OnRelease?.Invoke(new FingerReleaseInfo(info.TouchStartPosition, touchInfo.Position));
                    return;
                }

                DragInfo = Option<FingerDraggingInfo>.Some(new FingerDraggingInfo(
                    info.TouchStartPosition,
                    touchInfo.Position
                ));
            }
        }

        public Action<FingerTouchInfo> OnTouch { get; set; }
        public Option<FingerDraggingInfo> DragInfo { get; private set; }
        public Action<FingerReleaseInfo> OnRelease { get; set; }

        private InputSystem_Actions.PlayerActions PlayerInputActions { get; }

        public void Dispose()
        {
            PlayerInputActions.Disable();
        }
    }
}
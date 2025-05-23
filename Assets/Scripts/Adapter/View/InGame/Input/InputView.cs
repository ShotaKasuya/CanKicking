using System;
using Adapter.IView.Input;
using Adapter.View.Util;
using Module.Option;
using Structure.Util.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Adapter.View.InGame.Input
{
    /// <summary>
    /// 特例クラス
    /// `InputSystem`が生成するクラスのラッパ
    /// 生成コードにインターフェースをつけれないので
    /// </summary>
    public class InputView : IStartable, ITickable, IFingerView, IDisposable
    {
        [Inject]
        public InputView()
        {
            var inputSystemActions = new InputSystem_Actions();
            PlayerInputActions = inputSystemActions.Player;
        }

        public void Start()
        {
            PlayerInputActions.Enable();
            PlayerInputActions.Touch.performed += OnStartClick;
            PlayerInputActions.Touch.canceled += OnReleaseInput;
        }

        private void OnStartClick(InputAction.CallbackContext context)
        {
            if (DragInfo.IsSome) return;
            
            var touchPosition = PlayerInputActions.Position.ReadValue<Vector2>();

            OnTouch?.Invoke(new FingerTouchInfo(touchPosition));
            DragInfo = Option<FingerDraggingInfo>.Some(new FingerDraggingInfo(
                touchPosition, touchPosition
            ));
        }

        private void OnReleaseInput(InputAction.CallbackContext context)
        {
            if (DragInfo.IsNone) return;
            var releasePosition = PlayerInputActions.Position.ReadValue<Vector2>();
            var touchPosition = DragInfo.Unwrap();

            OnRelease?.Invoke(new FingerReleaseInfo(touchPosition.TouchStartPosition, releasePosition));
            DragInfo = Option<FingerDraggingInfo>.None();
        }

        public void Tick()
        {
            if (DragInfo.TryGetValue(out var draggingInfo))
            {
                var currentPosition = PlayerInputActions.Position.ReadValue<Vector2>();

                DragInfo = Option<FingerDraggingInfo>.Some(new FingerDraggingInfo(
                    draggingInfo.TouchStartPosition, currentPosition
                ));
            }
        }

        public Action<FingerTouchInfo> OnTouch { get; set; }
        public Option<FingerDraggingInfo> DragInfo { get; private set; }
        public Action<FingerReleaseInfo> OnRelease { get; set; }

        private InputSystem_Actions.PlayerActions PlayerInputActions { get; }

        public void Dispose()
        {
            PlayerInputActions.Touch.performed -= OnStartClick;
            PlayerInputActions.Touch.canceled -= OnReleaseInput;
            PlayerInputActions.Disable();
        }
    }
}
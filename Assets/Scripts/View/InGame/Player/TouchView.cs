using System;
using Interface.InGame.Player;
using Module.Option;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace View.InGame.Player
{
    public class TouchView: ITickable, IStartable, ITouchView, IDisposable
    {
        public TouchView
        (
            InputSystem_Actions inputSystem
        )
        {
            PlayerInputActions = inputSystem.Player;

            TouchSubject = new Subject<TouchStartEventArgument>();
            TouchEndSubject = new Subject<TouchEndEventArgument>();
        }

        public void Start()
        {
            PlayerInputActions.Enable();
            PlayerInputActions.Touch.performed += OnStartClick;
            PlayerInputActions.Touch.canceled += OnReleaseInput;
        }

        private void OnStartClick(InputAction.CallbackContext context)
        {
            Debug.Log("start");
            _pendingTouchStarted = true;
        }

        private void OnReleaseInput(InputAction.CallbackContext context)
        {
            Debug.Log("release");
            if (_fingerDraggingInfo.IsNone) return;
            var releasePosition = PlayerInputActions.Position.ReadValue<Vector2>();
            var touchPosition = _fingerDraggingInfo.Unwrap();

            TouchEndSubject.OnNext(new TouchEndEventArgument(touchPosition.TouchStartPosition, releasePosition));
            _fingerDraggingInfo = Option<FingerDraggingInfo>.None();
        }

        public void Tick()
        {
            Debug.Log(_pendingTouchStarted);
            if (_fingerDraggingInfo.TryGetValue(out var draggingInfo))
            {
                var currentPosition = PlayerInputActions.Position.ReadValue<Vector2>();

                _fingerDraggingInfo = Option<FingerDraggingInfo>.Some(new FingerDraggingInfo(
                    draggingInfo.TouchStartPosition, currentPosition
                ));
            }

            if (_pendingTouchStarted)
            {
                StartClick();
                Debug.Log("VAR");
                _pendingTouchStarted = false;
            }
        }

        private void StartClick()
        {
            Debug.Log("touch event");
            var isSome = _fingerDraggingInfo.IsSome;
            var isOnUi = EventSystem.current.IsPointerOverGameObject();
            if (isSome) return;
            // UI上のタッチは無視する
            if (isOnUi) return;

            var touchPosition = PlayerInputActions.Position.ReadValue<Vector2>();

            Debug.Log("touch event");
            TouchSubject.OnNext(new TouchStartEventArgument(touchPosition));
            _fingerDraggingInfo = Option<FingerDraggingInfo>.Some(new FingerDraggingInfo(
                touchPosition, touchPosition
            ));
        }
        
        public Observable<TouchStartEventArgument> TouchEvent => TouchSubject;
        public FingerDraggingInfo DraggingInfo => _fingerDraggingInfo.Unwrap();
        public Observable<TouchEndEventArgument> TouchEndEvent => TouchEndSubject;

        private bool _pendingTouchStarted;
        private InputSystem_Actions.PlayerActions PlayerInputActions { get; }
        private Subject<TouchStartEventArgument> TouchSubject { get; }
        private Subject<TouchEndEventArgument> TouchEndSubject { get; }
        private Option<FingerDraggingInfo> _fingerDraggingInfo;

        public void Dispose()
        {
            PlayerInputActions.Disable();
            TouchSubject?.Dispose();
            TouchEndSubject?.Dispose();
            
            PlayerInputActions.Touch.performed -= OnStartClick;
            PlayerInputActions.Touch.canceled -= OnReleaseInput;
            PlayerInputActions.Disable();
        }
    }
}
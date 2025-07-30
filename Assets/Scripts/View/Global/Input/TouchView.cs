using System.Collections.Generic;
using Interface.Global.Input;
using Module.Option;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace View.Global.Input
{
    public partial class GlobalInputView
    {
        private void OnStartClick(InputAction.CallbackContext context)
        {
            _pendingTouchStarted = true;
        }

        private void OnReleaseInput(InputAction.CallbackContext context)
        {
            if (_fingerDraggingInfo.IsNone) return;
            var releasePosition = Actions.Position.ReadValue<Vector2>();
            var touchPosition = _fingerDraggingInfo.Unwrap();

            TouchEndSubject.OnNext(new TouchEndEventArgument(touchPosition.TouchStartPosition, releasePosition));
            _fingerDraggingInfo = Option<FingerDraggingInfo>.None();
        }

        private void TouchTick()
        {
            if (_fingerDraggingInfo.TryGetValue(out var draggingInfo))
            {
                var secondTouch = Actions.SecondTouch.ReadValue<float>() > 0;
                if (secondTouch)
                {
                    _fingerDraggingInfo = Option<FingerDraggingInfo>.None();
                    return;
                }

                var currentPosition = Actions.Position.ReadValue<Vector2>();

                _fingerDraggingInfo = Option<FingerDraggingInfo>.Some(new FingerDraggingInfo(
                    draggingInfo.TouchStartPosition, currentPosition
                ));
            }

            if (_pendingTouchStarted)
            {
                StartClick();
                _pendingTouchStarted = false;
            }
        }

        private void StartClick()
        {
            var isSome = _fingerDraggingInfo.IsSome;
            var touchPosition = Actions.Position.ReadValue<Vector2>();
            var isOnUi = IsOverUi(touchPosition);
            if (isSome | isOnUi) return;

            TouchSubject.OnNext(new TouchStartEventArgument(touchPosition));
            _fingerDraggingInfo = Option<FingerDraggingInfo>.Some(new FingerDraggingInfo(
                touchPosition, touchPosition
            ));
        }

        private bool IsOverUi(Vector2 position)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = position
            };
            EventSystem.current?.RaycastAll(eventData, RaycastPool);
            foreach (var raycastResult in RaycastPool)
            {
                if (raycastResult.gameObject.TryGetComponent(out Button _))
                {
                    return true;
                }
            }

            return false;
        }

        public Observable<TouchStartEventArgument> TouchEvent => TouchSubject;
        public Option<FingerDraggingInfo> DraggingInfo => _fingerDraggingInfo;
        public Observable<TouchEndEventArgument> TouchEndEvent => TouchEndSubject;

        private List<RaycastResult> RaycastPool { get; } = new List<RaycastResult>(8);
        private bool _pendingTouchStarted;
        private Option<FingerDraggingInfo> _fingerDraggingInfo;
        private Subject<TouchStartEventArgument> TouchSubject { get; } = new Subject<TouchStartEventArgument>();
        private Subject<TouchEndEventArgument> TouchEndSubject { get; } = new Subject<TouchEndEventArgument>();
    }
}
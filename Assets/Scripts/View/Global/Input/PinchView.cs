using R3;
using UnityEngine.InputSystem;

namespace View.Global.Input
{
    public partial class GlobalInputView
    {
        public float Pool()
        {
            var input = Actions.Pinch;
            var pinchValue = input.ReadValue<float>();

            return pinchValue;
        }

        private void OnDoubleTap(InputAction.CallbackContext callbackContext)
        {
            DoubleTapSubject.OnNext(Unit.Default);
        }

        public Observable<Unit> DoubleTapEvent => DoubleTapSubject;

        private Subject<Unit> DoubleTapSubject { get; } = new Subject<Unit>();
    }
}
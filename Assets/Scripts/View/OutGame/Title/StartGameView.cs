using System;
using Interface.OutGame.Title;
using Module.DebugConsole;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;
using View.Global.Input;
using Vector2 = System.Numerics.Vector2;

namespace View.OutGame.Title
{
    public class StartGameView : IStartGameView, ITickable, IDisposable
    {
        public StartGameView(InputSystem_Actions inputSystemActions)
        {
            Actions = inputSystemActions.Player;
            Actions.Enable();

            StartSubject = new Subject<Unit>();
        }

        public Observable<Unit> StartEvent => StartSubject;

        private void OnTouch(InputAction.CallbackContext context)
        {
            StartSubject.OnNext(Unit.Default);
        }

        private Subject<Unit> StartSubject { get; }
        private InputSystem_Actions.PlayerActions Actions { get; }

        public void Dispose()
        {
            StartSubject?.Dispose();
            Actions.Touch.performed -= OnTouch;
            Actions.Disable();
        }

        public void Tick()
        {
            var primaryTouch = Actions.Position.ReadValue<Vector2>();
            var touch0 = Actions.Touch0.ReadValue<Vector2>();
            var touch1 = Actions.Touch1.ReadValue<Vector2>();
            var touch2 = Actions.Touch2.ReadValue<Vector2>();
            var touch3 = Actions.Touch3.ReadValue<Vector2>();
            var touch4 = Actions.Touch4.ReadValue<Vector2>();
            var touch5 = Actions.Touch5.ReadValue<Vector2>();
            var touch6 = Actions.Touch6.ReadValue<Vector2>();
            var touch7 = Actions.Touch7.ReadValue<Vector2>();
            var touch8 = Actions.Touch8.ReadValue<Vector2>();
            var touch9 = Actions.Touch9.ReadValue<Vector2>();
            
            DebugTextView.Instance.SetText("primaryTouch", primaryTouch);
            DebugTextView.Instance.SetText("Touch0", touch0);
            DebugTextView.Instance.SetText("Touch1", touch1);
            DebugTextView.Instance.SetText("Touch2", touch2);
            DebugTextView.Instance.SetText("Touch3", touch3);
            DebugTextView.Instance.SetText("Touch4", touch4);
            DebugTextView.Instance.SetText("Touch5", touch5);
            DebugTextView.Instance.SetText("Touch6", touch6);
            DebugTextView.Instance.SetText("Touch7", touch7);
            DebugTextView.Instance.SetText("Touch8", touch8);
            DebugTextView.Instance.SetText("Touch9", touch9);
        }
    }
}
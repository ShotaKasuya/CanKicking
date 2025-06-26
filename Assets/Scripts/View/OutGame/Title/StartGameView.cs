using System;
using Interface.OutGame.Title;
using R3;
using UnityEngine.InputSystem;
using VContainer.Unity;
using View.InGame.Player;

namespace View.OutGame.Title
{
    public class StartGameView : IStartGameView, IStartable, IDisposable
    {
        public StartGameView(InputSystem_Actions inputSystemActions)
        {
            Actions = inputSystemActions.Player;

            StartSubject = new Subject<Unit>();
        }

        public Observable<Unit> StartEvent => StartSubject;

        public void Start()
        {
            Actions.Enable();
            Actions.Touch.performed += OnTouch;
        }

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
    }
}
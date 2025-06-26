using System;
using Interface.OutGame.StageSelect;
using Module.Option;
using Module.SceneReference;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;
using View.InGame.Player;

namespace View.OutGame.StageSelect
{
    public class SelectionView : IStageSelectionView, IStartable, IDisposable
    {
        public SelectionView(InputSystem_Actions inputSystemActions)
        {
            InputActions = inputSystemActions.StageSelect;
            SelectSubject = new Subject<Option<SceneReference>>();
        }

        public void Start()
        {
            _mainCamera = Camera.main!;
            InputActions.Enable();
            InputActions.Touch.performed += OnClick;
        }

        private void OnClick(InputAction.CallbackContext _)
        {
            var screenPosition = InputActions.TouchPosition.ReadValue<Vector2>();
            var worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);

            var castHit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (castHit.collider is not null)
            {
                var collider = castHit.collider;
                if (collider.TryGetComponent<ISceneGettableView>(out var sceneGettableView))
                {
                    SelectSubject.OnNext(Option<SceneReference>.Some(sceneGettableView.Scene));
                    return;
                }
            }

            SelectSubject.OnNext(Option<SceneReference>.None());
        }

        public Observable<Option<SceneReference>> SelectEvent => SelectSubject;

        private Camera _mainCamera;
        private Subject<Option<SceneReference>> SelectSubject { get; }
        private InputSystem_Actions.StageSelectActions InputActions { get; }

        public void Dispose()
        {
            SelectSubject?.Dispose();
            InputActions.Touch.performed -= OnClick;
            InputActions.Enable();
        }
    }
}
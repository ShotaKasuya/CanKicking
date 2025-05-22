using System;
using Adapter.IView.OutGame.StageSelect;
using Adapter.IView.Util.UI;
using Adapter.View.Util;
using Module.Option;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Adapter.View.OutGame.StageSelect
{
    public class SelectInputView : ISelectedStageView, IStartable, IDisposable
    {
        [Inject]
        public SelectInputView
        (
        )
        {
            var inputSystemActions = new InputSystem_Actions();
            StageSelectActions = inputSystemActions.StageSelect;
        }

        public void Start()
        {
            _mainCamera = Camera.main;
            StageSelectActions.Enable();
            StageSelectActions.Touch.performed += OnSelect;
        }

        private void OnSelect(InputAction.CallbackContext _)
        {
            var screenPosition = StageSelectActions.TouchPosition.ReadValue<Vector2>();
            var worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);

            var castHit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (castHit.collider is not null)
            {
                var collider = castHit.collider;
                if (collider.TryGetComponent<ISceneGettableView>(out var sceneGettableView))
                {
                    StageSelectEvent?.Invoke(Option<string>.Some(sceneGettableView.SceneName));
                    return;
                }
            }

            StageSelectEvent?.Invoke(Option<string>.None());
        }

        public Action<Option<string>> StageSelectEvent { get; set; }

        private Camera _mainCamera;
        private InputSystem_Actions.StageSelectActions StageSelectActions { get; }

        public void Dispose()
        {
            StageSelectActions.Touch.performed -= OnSelect;
            StageSelectActions.Disable();
        }
    }
}
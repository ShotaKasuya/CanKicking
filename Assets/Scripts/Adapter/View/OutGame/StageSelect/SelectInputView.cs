using System;
using Adapter.IView.OutGame.StageSelect;
using Adapter.IView.Util.UI;
using Adapter.View.Util;
using Module.Option;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Adapter.View.OutGame.StageSelect
{
    public class SelectInputView : ISelectedStageView, IStartable, IDisposable
    {
        public SelectInputView
        (
            InputSystem_Actions inputSystemActions
        )
        {
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
                Debug.Log("Click");
                var collider = castHit.collider;
                if (collider.TryGetComponent<ISceneGettableView>(out var sceneGettableView))
                {
                    Debug.Log("Event");
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
            Debug.Log("on dispose");
            StageSelectActions.Touch.performed -= OnSelect;
            StageSelectActions.Disable();
        }
    }
}
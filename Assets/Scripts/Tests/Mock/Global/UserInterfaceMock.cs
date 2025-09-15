using Cysharp.Threading.Tasks;
using Interface.Model.Global;
using Interface.View.Global;
using Structure.Global.TimeScale;
using UnityEngine;

namespace Tests.Mock.Global
{
    public class MockTouchPositionUiView : ITouchPositionUiView
    {
        public bool IsFadeInCalled { get; private set; }
        public bool IsFadeOutCalled { get; private set; }
        public Vector2? FadeInPosition { get; private set; }

        public UniTask FadeIn(Vector2 screenPosition)
        {
            IsFadeInCalled = true;
            FadeInPosition = screenPosition;
            return UniTask.CompletedTask;
        }

        public UniTask FadeOut()
        {
            IsFadeOutCalled = true;
            return UniTask.CompletedTask;
        }
    }

    public class MockLoadingPanelView : ILoadingPanelView
    {
        public bool IsShowPanelCalled { get; private set; }
        public bool IsHidePanelCalled { get; private set; }

        public UniTask ShowPanel()
        {
            IsShowPanelCalled = true;
            return UniTask.CompletedTask;
        }

        public UniTask HidePanel()
        {
            IsHidePanelCalled = true;
            return UniTask.CompletedTask;
        }
    }

    public class MockTimeScaleModel : ITimeScaleModel
    {
        public TimeCommandType? ExecutedCommand { get; private set; }
        public bool IsUndoCalled { get; private set; }
        public bool IsResetCalled { get; private set; }
        public void Execute(TimeCommandType timeCommand) => ExecutedCommand = timeCommand;
        public void Undo() => IsUndoCalled = true;
        public void Reset() => IsResetCalled = true;
    }
}
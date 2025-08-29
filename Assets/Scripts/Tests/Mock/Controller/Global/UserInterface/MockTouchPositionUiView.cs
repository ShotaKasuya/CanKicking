using Cysharp.Threading.Tasks;
using Interface.View.Global;
using UnityEngine;

namespace Tests.Mock.Controller.Global.UserInterface
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
}

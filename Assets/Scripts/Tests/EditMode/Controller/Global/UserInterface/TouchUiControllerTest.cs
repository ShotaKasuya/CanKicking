
using Controller.Global.UserInterface;
using Cysharp.Threading.Tasks;
using Interface.Global;
using Interface.View.Global;
using Module.Option.Runtime;
using NUnit.Framework;
using R3;
using UnityEngine;

namespace Tests.EditMode.Controller.Global.UserInterface
{
    public class TouchUiControllerTest
    {
        // Mocks
        private class MockTouchView : ITouchView
        {
            private readonly Subject<TouchStartEventArgument> _touchSubject = new();
            private readonly Subject<TouchEndEventArgument> _touchEndSubject = new();
            public Observable<TouchStartEventArgument> TouchEvent => _touchSubject;
            public Option<FingerDraggingInfo> DraggingInfo => Option<FingerDraggingInfo>.None();
            public Observable<TouchEndEventArgument> TouchEndEvent => _touchEndSubject;

            public void SimulateTouch(Vector2 position) => _touchSubject.OnNext(new TouchStartEventArgument(position));
            public void SimulateTouchEnd() => _touchEndSubject.OnNext(new TouchEndEventArgument());
        }

        private class MockTouchPositionUiView : ITouchPositionUiView
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

        private TouchUiController _controller;
        private MockTouchView _touchView;
        private MockTouchPositionUiView _touchPositionUiView;

        [SetUp]
        public void SetUp()
        {
            _touchView = new MockTouchView();
            _touchPositionUiView = new MockTouchPositionUiView();
            _controller = new TouchUiController(_touchView, _touchPositionUiView);
            _controller.Start();
        }

        [Test]
        public void OnTouchEvent_CallsFadeInWithCorrectPosition()
        {
            var touchPosition = new Vector2(100, 200);
            _touchView.SimulateTouch(touchPosition);

            Assert.IsTrue(_touchPositionUiView.IsFadeInCalled);
            Assert.AreEqual(touchPosition, _touchPositionUiView.FadeInPosition);
        }

        [Test]
        public void OnTouchEndEvent_CallsFadeOut()
        {
            _touchView.SimulateTouchEnd();

            Assert.IsTrue(_touchPositionUiView.IsFadeOutCalled);
        }
    }
}

using Controller.Global.UserInterface;
using NUnit.Framework;
using Tests.Mock.Controller.Global.UserInterface;
using Tests.Mock.Global;
using UnityEngine;

namespace Tests.EditMode.Controller.Global.UserInterface
{
    public class TouchUiControllerTest
    {
        

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
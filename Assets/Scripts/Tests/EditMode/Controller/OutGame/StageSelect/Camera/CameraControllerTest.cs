
using Controller.OutGame.StageSelect.Camera;
using Interface.Global.Input;
using Interface.Global.Utility;
using Interface.OutGame.StageSelect;
using Module.Option.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.Controller.OutGame.StageSelect.Camera
{
    public class CameraControllerTest
    {
        // Mocks
        private class MockTouchView : ITouchView
        {
            public R3.Observable<TouchStartEventArgument> TouchEvent => R3.Observable.Empty<TouchStartEventArgument>();
            public Option<FingerDraggingInfo> DraggingInfo { get; set; } = Option<FingerDraggingInfo>.None();
            public R3.Observable<TouchEndEventArgument> TouchEndEvent => R3.Observable.Empty<TouchEndEventArgument>();
        }

        private class MockCameraPositionView : ICameraPositionView
        {
            public Vector2? ForceAdded { get; private set; }
            public void AddForce(Vector2 vector2) => ForceAdded = vector2;
        }

        private class MockScreenScaleModel : IScreenScaleModel
        {
            public Vector2 Scale { get; set; } = new Vector2(1080, 1920);
            public float Width => Scale.x;
            public float Height => Scale.y;
        }

        private CameraController _controller;
        private MockTouchView _touchView;
        private MockCameraPositionView _cameraPositionView;
        private MockScreenScaleModel _screenScaleModel;

        [SetUp]
        public void SetUp()
        {
            _touchView = new MockTouchView();
            _cameraPositionView = new MockCameraPositionView();
            _screenScaleModel = new MockScreenScaleModel();
            _controller = new CameraController(_touchView, _cameraPositionView, _screenScaleModel);
        }

        [Test]
        public void Tick_WithDragging_AddsForceToCamera()
        {
            // Arrange
            var dragDelta = new Vector2(0, 10);
            var draggingInfo = new FingerDraggingInfo(Vector2.zero, dragDelta, dragDelta);
            _touchView.DraggingInfo = Option<FingerDraggingInfo>.Some(draggingInfo);

            // Act
            _controller.Tick();

            // Assert
            Assert.IsTrue(_cameraPositionView.ForceAdded.HasValue);
            var expectedForce = Vector2.down * dragDelta / _screenScaleModel.Height;
            Assert.AreEqual(expectedForce, _cameraPositionView.ForceAdded.Value);
        }

        [Test]
        public void Tick_WithoutDragging_DoesNotAddForce()
        {
            // Arrange
            _touchView.DraggingInfo = Option<FingerDraggingInfo>.None();

            // Act
            _controller.Tick();

            // Assert
            Assert.IsFalse(_cameraPositionView.ForceAdded.HasValue);
        }
    }
}

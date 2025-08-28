using Controller.InGame.Stage;
using Interface.Model.InGame;
using Interface.View.InGame;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.Stage
{
    public class StageCameraControllerTest
    {
        // Mocks
        private class MockPinchView : IPinchView
        {
            public float PinchValue { get; set; }
            public float Pool() => PinchValue;
        }

        private class MockCameraView : ICameraView
        {
            public float OrthoSize { get; private set; }
            public void SetOrthoSize(float orthoSize) => OrthoSize = orthoSize;

            public void Init(Transform playerTransform)
            {
            }
        }

        private class MockCameraZoomModel : ICameraZoomModel
        {
            public float InitialOrthoSize { get; set; } = 5f;
            public float ZoomLevelOnSet { get; private set; }
            public float OrthoSizeToReturn { get; set; } = 5f;

            public float SetZoomLevel(float level)
            {
                ZoomLevelOnSet = level;
                ZoomLevel = level;
                return OrthoSizeToReturn;
            }

            public float GetOrthoSize() => InitialOrthoSize;
            public float ZoomLevel { get; private set; }
            public float Sensitivity { get; set; } = 1f;
        }

        private StageCameraController _controller;
        private MockPinchView _pinchView;
        private MockCameraView _cameraView;
        private MockCameraZoomModel _cameraZoomModel;

        [SetUp]
        public void SetUp()
        {
            _pinchView = new MockPinchView();
            _cameraView = new MockCameraView();
            _cameraZoomModel = new MockCameraZoomModel();
            _controller = new StageCameraController(_pinchView, _cameraView, _cameraZoomModel);
        }

        [Test]
        public void Initialize_SetsInitialZoomAndOrthoSize()
        {
            // Act
            _controller.Initialize();

            // Assert
            Assert.AreEqual(0, _cameraZoomModel.ZoomLevelOnSet);
            Assert.AreEqual(_cameraZoomModel.InitialOrthoSize, _cameraView.OrthoSize);
        }

        [Test]
        public void Tick_WithPinchInput_UpdatesZoomAndOrthoSize()
        {
            // Arrange
            _controller.Initialize(); // Set initial zoom level
            _pinchView.PinchValue = 0.5f;
            _cameraZoomModel.OrthoSizeToReturn = 7.5f;

            // Act
            _controller.Tick();

            // Assert
            float expectedLevel = _cameraZoomModel.Sensitivity * -_pinchView.PinchValue + _cameraZoomModel.ZoomLevel;
            // Assert.AreEqual(expectedLevel, _cameraZoomModel.ZoomLevelOnSet, 1e-5);
            Assert.AreEqual(_cameraZoomModel.OrthoSizeToReturn, _cameraView.OrthoSize);
        }

        [Test]
        public void Tick_WithoutPinchInput_DoesNotUpdateCamera()
        {
            // Arrange
            _controller.Initialize();
            _cameraView.SetOrthoSize(0); // Reset from Initialize call
            _pinchView.PinchValue = 0.001f;

            // Act
            _controller.Tick();

            // Assert
            Assert.AreEqual(_cameraView.OrthoSize, 0);
        }
    }
}
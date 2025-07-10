using Interface.InGame.Stage;
using UnityEngine;
using VContainer.Unity;

namespace Controller.InGame
{
    public class StageCameraController : ITickable
    {
        public StageCameraController
        (
            IPinchView pinchView,
            ICameraView cameraView,
            ICameraZoomModel cameraZoomModel
        )
        {
            PinchView = pinchView;
            CameraView = cameraView;
            CameraZoomModel = cameraZoomModel;
        }

        public void Tick()
        {
            var pinchValue = -PinchView.Pool();
            if (Mathf.Abs(pinchValue) > 0.01f)
            {
                var sensi = CameraZoomModel.Sensitivity;
                var currentLevel = CameraZoomModel.ZoomLevel;
                var nextLevel = sensi * pinchValue + currentLevel;
                
                var orthoSize = CameraZoomModel.SetZoomLevel(nextLevel);
                CameraView.SetOrthoSize(orthoSize);
            }
        }

        private IPinchView PinchView { get; }
        private ICameraView CameraView { get; }
        private ICameraZoomModel CameraZoomModel { get; }
    }
}
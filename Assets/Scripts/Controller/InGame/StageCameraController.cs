using Interface.InGame.Stage;
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
        }

        private IPinchView PinchView { get; }
        private ICameraView CameraView { get; }
        private ICameraZoomModel CameraZoomModel { get; }
    }
}
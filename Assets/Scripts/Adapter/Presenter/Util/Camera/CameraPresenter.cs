using Adapter.IView.Util.Camera;
using Domain.IPresenter.Util.Camera;

namespace Adapter.Presenter.Util.Camera
{
    public class CameraPresenter : ICameraOrthographicSizePresenter
    {
        public CameraPresenter
        (
            ICameraView cameraView
        )
        {
            CameraView = cameraView;
        }

        public void SetOrthographicSize(float size)
        {
            CameraView.SetOrthographicSize(size);
        }

        private ICameraView CameraView { get; }
    }
}
using System;
using Domain.IPresenter.Util.Camera;
using Domain.IRepository.Util;
using VContainer.Unity;

namespace Domain.UseCase.InGame.Stage
{
    public class CameraFitScreenCase : IStartable, IDisposable
    {
        public CameraFitScreenCase
        (
            IScreenScaleRepository screenScaleRepository,
            IScreenWidthRepository screenWidthRepository,
            ICameraOrthographicSizePresenter cameraOrthographicSizePresenter
        )
        {
            ScreenScaleRepository = screenScaleRepository;
            ScreenWidthRepository = screenWidthRepository;
            CameraOrthographicSizePresenter = cameraOrthographicSizePresenter;
        }

        public void Start()
        {
            ScreenWidthRepository.OnWidthChange += FitCameraScale;

            FitCameraScale();
        }

        private void FitCameraScale()
        {
            var screenScale = ScreenScaleRepository.ScreenScale;
            var aspectRatio = screenScale.x / screenScale.y;
            var targetWidth = ScreenWidthRepository.Width;

            var orthographicSize = targetWidth / (2 * aspectRatio);

            CameraOrthographicSizePresenter.SetOrthographicSize(orthographicSize);
        }

        private IScreenScaleRepository ScreenScaleRepository { get; }
        private IScreenWidthRepository ScreenWidthRepository { get; }
        private ICameraOrthographicSizePresenter CameraOrthographicSizePresenter { get; }

        public void Dispose()
        {
            ScreenWidthRepository.OnWidthChange -= FitCameraScale;
        }
    }
}
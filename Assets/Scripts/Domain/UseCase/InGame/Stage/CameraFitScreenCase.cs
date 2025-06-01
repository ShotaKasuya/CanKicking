using System;
using Domain.IPresenter.Util.Camera;
using Domain.IRepository.Util;
using R3;
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

            CompositeDisposable = new CompositeDisposable();
        }

        public void Start()
        {
            ScreenWidthRepository.ReactiveWidthWeight.Subscribe(FitCameraScale).AddTo(CompositeDisposable);
            FitCameraScale(ScreenWidthRepository.WidthWeight);
        }

        private void FitCameraScale(float weight)
        {
            var screenScale = ScreenScaleRepository.ScreenScale;
            var aspectRatio = screenScale.x / screenScale.y;
            var width = ScreenWidthRepository.GetScreenWidth(weight);

            var orthographicSize = width / (2 * aspectRatio);

            CameraOrthographicSizePresenter.SetOrthographicSize(orthographicSize);
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IScreenScaleRepository ScreenScaleRepository { get; }
        private IScreenWidthRepository ScreenWidthRepository { get; }
        private ICameraOrthographicSizePresenter CameraOrthographicSizePresenter { get; }

        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}
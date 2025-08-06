using Controller.InGame.Stage;
using GoogleMobileAds.Api;
using Model.InGame.Stage;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.Stage;

namespace Installer.InGame.Stage
{
    public class StageInstaller: LifetimeScope
    {
        [SerializeField] private BaseHeightView baseHeightView;
        [SerializeField] private SpawnPositionView spawnPositionView;
        [SerializeField] private GoalView goalView;
        [SerializeField] private CameraView cameraView;
        [SerializeField] private CameraZoomModel cameraZoomModel;
        [SerializeField] private FallLineModel fallLineModel;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(baseHeightView).AsImplementedInterfaces();
            builder.RegisterInstance(spawnPositionView).AsImplementedInterfaces();
            builder.RegisterInstance(goalView).AsImplementedInterfaces();
            builder.RegisterInstance(cameraView).AsImplementedInterfaces();
            builder.Register<BannerView>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Model
            builder.RegisterInstance(cameraZoomModel).AsImplementedInterfaces();
            builder.RegisterInstance(fallLineModel).AsImplementedInterfaces();
            
            // Controller
            builder.UseEntryPoints(pointsBuilder =>
            {
                pointsBuilder.Add<StageCameraController>();
                pointsBuilder.Add<StageInitializeController>();
                pointsBuilder.Add<RespawnPlayerController>();
            });
        }
    }
}
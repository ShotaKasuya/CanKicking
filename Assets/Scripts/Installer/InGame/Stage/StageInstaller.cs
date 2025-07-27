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
        [SerializeField] private PlayerSpawnPositionView spawnPositionView;
        [SerializeField] private GoalView goalView;
        [SerializeField] private CameraView cameraView;
        [SerializeField] private CameraZoomModel cameraZoomModel;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(spawnPositionView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(goalView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(cameraView).AsImplementedInterfaces();
            });
            builder.Register<BannerView>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Model
            builder.RegisterInstance(cameraZoomModel).AsImplementedInterfaces();
            
            // Controller
            builder.UseEntryPoints(pointsBuilder =>
            {
                pointsBuilder.Add<StageCameraController>();
                pointsBuilder.Add<StageInitializeController>();
            });
        }
    }
}
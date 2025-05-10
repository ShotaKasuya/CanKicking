using Adapter.DataStore.Setting;
using Adapter.Presenter.InGame.Stage;
using Adapter.Presenter.Util.Camera;
using Adapter.Repository.Setting;
using Adapter.View.InGame.Stage;
using Adapter.View.Util;
using Domain.UseCase.InGame.Stage;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.InGame
{
    public class StageInstaller : LifetimeScope
    {
        [SerializeField] private SpawnPositionView spawnPositionView;
        [SerializeField] private GoalView goalView;
        [SerializeField] private ScreenDataStore screenDataStore;
        [SerializeField] private CameraView playerFollowCamera;

        protected override void Configure(IContainerBuilder builder)
        {
            // Presenter
            builder.Register<SpawnPositionPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GoalEventPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CameraPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Repository
            builder.Register<ScreenRepository>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // UseCase
            builder.Register<CameraFitScreenCase>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
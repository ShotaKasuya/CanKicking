using Adapter.Presenter.InGame.Stage;
using Adapter.Presenter.Util.Camera;
using Adapter.Repository.Setting;
using Detail.DataStore.Setting;
using Detail.View.InGame.Stage;
using Detail.View.Util;
using Domain.IPresenter.InGame.Stage;
using Domain.UseCase.InGame.Stage;
using Module.Installer;
using UnityEngine;

namespace Installer.InGame
{
    public class StageInstaller : InstallerBase
    {
        [SerializeField] private SpawnPositionView spawnPositionView;
        [SerializeField] private GoalView goalView;
        [SerializeField] private ScreenDataStore screenDataStore;
        [SerializeField] private CameraView playerFollowCamera;

        protected override void CustomConfigure()
        {
            // DataStore

            // Presenter
            var spawnPositionPresenter = new SpawnPositionPresenter(spawnPositionView);
            RegisterInstance<ISpawnPositionPresenter, SpawnPositionPresenter>(spawnPositionPresenter);
            var goalEventPresenter = new GoalEventPresenter(goalView);
            RegisterInstance<IGoalEventPresenter, GoalEventPresenter>(goalEventPresenter);
            var cameraPresenter = new CameraPresenter(playerFollowCamera);

            // Repository
            var screenRepository = new ScreenRepository(screenDataStore);

            // UseCase
            var cameraFitScreenCase = new CameraFitScreenCase(screenRepository, screenRepository, cameraPresenter);
            RegisterEntryPoints(cameraFitScreenCase);
        }
    }
}
using Adapter.Presenter.InGame.UI;
using Detail.View.InGame.UI;
using Domain.IPresenter.InGame.Player;
using Domain.IPresenter.InGame.Stage;
using Domain.IPresenter.Util.Input;
using Domain.IRepository.InGame.Player;
using Domain.UseCase.InGame.Stage;
using Domain.UseCase.InGame.UI;
using Module.Installer;
using UnityEngine;

namespace Installer.InGame
{
    public class UiInstaller: InstallerBase
    {
        [SerializeField] private InstallerBase playerInstaller;
        [SerializeField] private InstallerBase stageInstaller;

        [SerializeField] private PowerImageView powerView;
        [SerializeField] private HeightView heightView;
        [SerializeField] private GoalMessageView goalMessageView;

        protected override void CustomConfigure()
        {
            // Presenter
            var kickPowerPresenter = new KickPowerPresenter(powerView);
            var heightPresenter = new HeightPresenter(heightView);
            var goalMessagePresenter = new GoalPresenter(goalMessageView);
            var touchEventPresenter = GlobalLocator.Instance.GetInstance<IFingerTouchEventPresenter>();
            var touchingEventPresenter = GlobalLocator.Instance.GetInstance<IFingerTouchingEventPresenter>();
            var releaseEventPresenter = GlobalLocator.Instance.GetInstance<IFingerReleaseEventPresenter>();
            var playerPresenter = playerInstaller.GetInstance<IPlayerPresenter>();
            var spawnPointPresenter = stageInstaller.GetInstance<ISpawnPositionPresenter>();
            var goalEventPresenter = stageInstaller.GetInstance<IGoalEventPresenter>();

            // Repository
            var kickPowerRepository = playerInstaller.GetInstance<IKickPowerRepository>();

            var showPowerCase = new ShowPowerCase(kickPowerPresenter, touchEventPresenter, touchingEventPresenter,
                releaseEventPresenter, kickPowerRepository);
            RegisterEntryPoints(showPowerCase);
            var showHeightCase = new ShowHeightCase(heightPresenter, playerPresenter, spawnPointPresenter);
            RegisterEntryPoints(showHeightCase);
            var goalCase = new GoalCase(goalEventPresenter, goalMessagePresenter);
            RegisterEntryPoints(goalCase);
        }
    }
}
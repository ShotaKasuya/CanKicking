using Adapter.Presenter.InGame.UI;
using Detail.View.InGame.UI;
using Domain.IPresenter.Util.Input;
using Domain.IRepository.InGame.Player;
using Domain.UseCase.InGame.UI;
using UnityEngine;

namespace Installer.InGame
{
    public class UiInstaller: InstallerBase
    {
        [SerializeField] private InstallerBase playerInstaller;

        [SerializeField] private PowerImageView powerView;

        protected override void CustomConfigure()
        {
            // Presenter
            var kickPowerPresenter = new KickPowerPresenter(powerView);
            var touchEventPresenter = playerInstaller.GetInstance<IFingerTouchEventPresenter>();
            var touchingEventPresenter = playerInstaller.GetInstance<IFingerTouchingEventPresenter>();
            var releaseEventPresenter = playerInstaller.GetInstance<IFingerReleaseEventPresenter>();

            // Repository
            var kickPowerRepository = playerInstaller.GetInstance<IKickPowerRepository>();

            var showPowerCase = new ShowPowerCase(kickPowerPresenter, touchEventPresenter, touchingEventPresenter,
                releaseEventPresenter, kickPowerRepository);
            RegisterEntryPoints(showPowerCase);
        }
    }
}
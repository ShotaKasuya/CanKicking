using Adapter.Presenter.InGame.Player;
using Adapter.Presenter.Util.Input;
using Adapter.Repository.InGame.Player;
using DataUtil.InGame.Player;
using Detail.DataStore.InGame.Player;
using Detail.View.InGame.Input;
using Detail.View.InGame.Player;
using Domain.IPresenter.Util.Input;
using Domain.IRepository.InGame.Player;
using Domain.UseCase.InGame.Player;
using UnityEngine;

namespace Installer.InGame
{
    public class PlayerInstaller: InstallerBase
    {
        [SerializeField] private PlayerStatusDataObject playerStatusDataObject;
        [SerializeField] private KickRandomConfig kickRandomConfig;
        
        protected override void CustomConfigure()
        {
            // view
            var playerView = GetComponent<PlayerView>();
            var inputAction = new InputView();
            RegisterEntryPoints(inputAction);
            
            // DataStore
            var playerStatusData = new PlayerStatusData(playerStatusDataObject);

            // Presenter
            var kickPresenter = new PlayerKickPresenter(playerView);
            var fingerEventPresenter = new FingerEventPresenter(inputAction);
            RegisterInstance<IFingerTouchEventPresenter, FingerEventPresenter>(fingerEventPresenter);
            RegisterInstance<IFingerTouchingEventPresenter, FingerEventPresenter>(fingerEventPresenter);
            RegisterInstance<IFingerReleaseEventPresenter, FingerEventPresenter>(fingerEventPresenter);
            RegisterEntryPoints(fingerEventPresenter);

            // Repository
            var kickPowerRepository = new PowerRepository();
            RegisterInstance<IKickPowerRepository, PowerRepository>(kickPowerRepository);
            var playerStatusRepository = new PlayerStatusRepository(playerStatusData);

            // UseCase
            var kickCase = new KickCase(kickPresenter, fingerEventPresenter, kickPowerRepository, playerStatusRepository);
            RegisterEntryPoints(kickCase);
            var powerRandomizer = new KickPowerRandomizationCase(kickPowerRepository, kickRandomConfig);
            RegisterEntryPoints(powerRandomizer);
        }
    }
}
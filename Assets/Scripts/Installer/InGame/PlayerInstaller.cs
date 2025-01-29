using Adapter.Presenter.InGame.Player;
using Adapter.Repository.InGame.Player;
using DataUtil.InGame.Player;
using Detail.DataStore.InGame.Player;
using Detail.View.InGame.Player;
using Domain.IPresenter.InGame.Player;
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
            
            // DataStore
            var playerStatusData = new PlayerStatusData(playerStatusDataObject);

            // Presenter
            var fingerEventPresenter = GlobalLocator.Resolve<IFingerReleaseEventPresenter>();
            var kickPresenter = new PlayerKickPresenter(playerView);
            var playerPresenter = new PlayerPresenter(playerView);
            RegisterInstance<IPlayerPresenter, PlayerPresenter>(playerPresenter);

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
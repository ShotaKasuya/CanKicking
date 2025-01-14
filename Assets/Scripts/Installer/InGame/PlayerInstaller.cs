using Adapter.Presenter.InGame.Player;
using Adapter.Presenter.Util.Input;
using Adapter.Repository.InGame.Player;
using DataUtil.InGame.Player;
using Detail.DataStore.InGame.Player;
using Detail.View.InGame.Input;
using Detail.View.InGame.Player;
using Domain.UseCase.InGame.Player;
using UnityEngine;

namespace Installer.InGame
{
    public class PlayerInstaller: InstallerBase
    {
        [SerializeField] private PlayerStatusDataObject playerStatusDataObject;
        [SerializeField] private KickRandomConfig kickRandomConfig;
        
        private void Awake()
        {
            // view
            var playerView = GetComponent<PlayerView>();
            var inputAction = new InputView();
            
            // DataStore
            var playerStatusData = new PlayerStatusData(playerStatusDataObject);

            // Presenter
            var kickPresenter = new PlayerKickPresenter(playerView);
            var fingerEventPresenter = new FingerEventPresenter(inputAction);
            RegisterEntryPoints(fingerEventPresenter);

            // Repository
            var kickPowerRepository = new PowerRepository();
            var playerStatusRepository = new PlayerStatusRepository(playerStatusData);

            // UseCase
            var kickCase = new KickCase(kickPresenter, fingerEventPresenter, kickPowerRepository, playerStatusRepository);
            var powerRandomizer = new KickPowerRandomizationCase(kickPowerRepository, kickRandomConfig);
            RegisterEntryPoints(powerRandomizer);
        }
    }
}
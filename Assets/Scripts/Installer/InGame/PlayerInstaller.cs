using Adapter.Presenter.InGame.Player;
using Adapter.Presenter.Util.Input;
using Adapter.Repository.InGame.Player;
using DataUtil.InGame.Player;
using Detail.View.InGame.Input;
using Detail.View.InGame.Player;
using Domain.UseCase.InGame.Player;
using UnityEngine;

namespace Installer.InGame
{
    public class PlayerInstaller: InstallerBase
    {
        [SerializeField] private PlayerInitialStatus playerInitialStatus;

        
        private void Awake()
        {
            // view
            var playerView = GetComponent<PlayerView>();
            var inputAction = new InputView();
            
            // DataStore

            // Presenter
            var kickPresenter = new PlayerKickPresenter(playerView);
            var fingerEventPresenter = new FingerEventPresenter(inputAction);
            RegisterEntryPoints(fingerEventPresenter);

            // Repository
            var kickPowerRepository = new PowerRepository();
            var playerStatusRepository = new PlayerStatusRepository();

            // UseCase
            var kickCase = new KickCase(kickPresenter, fingerEventPresenter, kickPowerRepository, playerStatusRepository);
        }
    }
}
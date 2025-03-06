using System.Collections.Generic;
using Adapter.Presenter.InGame.Player;
using Adapter.Repository.InGame.Player;
using DataUtil.InGame.Player;
using Detail.DataStore.InGame.Player;
using Detail.View.InGame.Player;
using Domain.IPresenter.InGame.Player;
using Domain.IPresenter.Util.Input;
using Domain.IRepository.InGame.Player;
using Domain.UseCase.InGame.Player;
using Module.Installer;
using Module.StateMachine;
using UnityEngine;

namespace Installer.InGame
{
    public class PlayerInstaller : InstallerBase
    {
        [SerializeField] private PlayerKickStatusDataStore playerStatusDataObject;

        protected override void CustomConfigure()
        {
            // view
            var playerView = GetComponent<PlayerView>();

            // Presenter
            var fingerEventPresenter = GlobalLocator.Instance.GetInstance<IFingerReleaseEventPresenter>();
            var kickPresenter = new PlayerKickPresenter(playerView);
            var playerPresenter = new PlayerPresenter(playerView);
            RegisterInstance<IPlayerPresenter, PlayerPresenter>(playerPresenter);

            // Repository
            var kickPowerRepository = new PowerRepository();
            RegisterInstance<IKickPowerRepository, PowerRepository>(kickPowerRepository);
            var playerStatusRepository = new PlayerStatusRepository(playerStatusDataObject, playerStatusDataObject);

            // Entity

            // UseCase
            var kickCase = new KickCase(PlayerStateType.Idle, kickPresenter, fingerEventPresenter, kickPowerRepository,
                playerStatusRepository);
            RegisterEntryPoints(kickCase);

            // StateMachine
            // var stateMachine = new PlayerStateMachine(
            //     playerStateEntity, new List<IStateBehaviour<PlayerStateType>>() { kickCase });
            // RegisterEntryPoints(stateMachine);
        }
    }
}
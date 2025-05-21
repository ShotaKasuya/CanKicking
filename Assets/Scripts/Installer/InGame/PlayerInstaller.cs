using Adapter.DataStore.InGame.Player;
using Adapter.Presenter.InGame.Player;
using Adapter.Presenter.Input;
using Adapter.Repository.InGame.Player;
using Adapter.View.InGame.Input;
using Adapter.View.InGame.Player;
using Domain.Entity.InGame.Player;
using Domain.IUseCase.InGame;
using Domain.UseCase.InGame.Player;
using Module.StateMachine;
using Structure.InGame.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.InGame
{
    public class PlayerInstaller : LifetimeScope
    {
        [SerializeField] private AimView aimView;
        [SerializeField] private PlayerKickStatusDataStore kickStatusDataStore;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            var playerView = GetComponent<PlayerView>();
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(playerView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(aimView).AsImplementedInterfaces();
            });
            builder.Register<InputView>(Lifetime.Singleton).AsImplementedInterfaces();

            // DataStore
            builder.RegisterInstance(kickStatusDataStore).AsImplementedInterfaces();
            builder.Register<PlayerConstantDataStore>(Lifetime.Singleton).AsImplementedInterfaces();

            // Presenter
            builder.Register<PlayerKickPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GroundPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TouchPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AimPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerContactPresenter>(Lifetime.Singleton).AsImplementedInterfaces();

            // Repository
            builder.Register<BasePowerRepository>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GroundingInfoRepository>(Lifetime.Singleton).AsImplementedInterfaces();

            // Entity
            builder.Register<IsGroundEntity>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IsStopedEntity>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CalcPowerEntity>(Lifetime.Singleton).AsImplementedInterfaces();

            // UseCase
            builder.Register<PlayerState>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<PlayerStateMachine>();
            builder.Register<PlayerIdleCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerAimingCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerFryingCase>(Lifetime.Singleton).AsImplementedInterfaces();
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            var state = Container.Resolve<IMutStateEntity<PlayerStateType>>();
            var style = new GUIStyle()
            {
                fontSize = 130
            };
            GUI.Label(new Rect(10, 10, 100, 20), state.State.ToString(), style);
        }
#endif
    }
}
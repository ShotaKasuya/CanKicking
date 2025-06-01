using Adapter.Presenter.InGame.Player;
using Adapter.Presenter.Input;
using Adapter.Repository.InGame.Player;
using Adapter.View.InGame.Player;
using Domain.Entity.InGame.Player;
using Domain.IRepository.InGame.Player;
using Domain.UseCase.InGame.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.InGame
{
    public class PlayerInstaller : LifetimeScope
    {
        [SerializeField] private AimView aimView;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            var playerView = GetComponent<PlayerView>();
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(playerView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(aimView).AsImplementedInterfaces();
            });
            builder.Register<InputView>(Lifetime.Scoped).AsImplementedInterfaces();

            // Presenter
            builder.Register<PlayerKickPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<GroundPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<TouchPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<AimPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerContactPresenter>(Lifetime.Scoped).AsImplementedInterfaces();

            // Repository
            builder.Register<PlayerStateRepository>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<BasePowerRepository>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<GroundingInfoRepository>(Lifetime.Scoped).AsImplementedInterfaces();

            // Entity
            builder.Register<ConvertScreenBaseVectorEntity>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<IsGroundEntity>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<IsStopedEntity>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<CalcPowerEntity>(Lifetime.Scoped).AsImplementedInterfaces();

            // UseCase
            builder.RegisterEntryPoint<PlayerStateMachine>();
            builder.Register<PlayerIdleCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerAimingCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerFryingCase>(Lifetime.Scoped).AsImplementedInterfaces();
        }

#if UNITY_EDITOR
        private IMutPlayerStateRepository _state;

        private void Start()
        {
            _state = Container.Resolve<IMutPlayerStateRepository>();
        }

        private void OnGUI()
        {
            var style = new GUIStyle()
            {
                fontSize = 130
            };
            GUI.Label(new Rect(10, 10, 100, 20), _state.State.ToString(), style);
        }
#endif
    }
}
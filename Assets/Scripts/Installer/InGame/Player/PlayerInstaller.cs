using Controller.InGame.Player;
using Model.InGame.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.Player;

namespace Installer.InGame.Player
{
    [RequireComponent(typeof(PlayerView))]
    public class PlayerInstaller : LifetimeScope
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private AimView aimView;
        [SerializeField] private PlayerModelBind playerModelBind;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(playerView).AsImplementedInterfaces();
            builder.RegisterComponent(aimView).AsImplementedInterfaces();

            // Model
            playerModelBind.Register(builder);
            builder.Register<KickPositionModel>(Lifetime.Singleton).AsImplementedInterfaces();

            // Controller
            builder.RegisterEntryPoint<InitializeController>();
            builder.RegisterEntryPoint<PlayerStateMachine>();
            builder.Register<IdleController>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<AimingController>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<FryingController>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}
using Controller.InGame.Player;
using Logic.InGame.Player;
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
        [SerializeField] private PlayerCollisionEffectViewFactory playerCollisionEffectViewFactory;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(playerView).AsImplementedInterfaces();
            builder.RegisterComponent(aimView).AsImplementedInterfaces();
            builder.RegisterInstance(playerCollisionEffectViewFactory).AsImplementedInterfaces();

            // Model
            playerModelBind.Register(builder);
            
            // Logic
            builder.Register<CalcByScreenRatioLogic>(Lifetime.Singleton).AsImplementedInterfaces();

            // Controller
            builder.RegisterEntryPoint<AnyStateController>();
            builder.RegisterEntryPoint<PlayerStateMachine>();
            builder.Register<IdleController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AimingController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FryingController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
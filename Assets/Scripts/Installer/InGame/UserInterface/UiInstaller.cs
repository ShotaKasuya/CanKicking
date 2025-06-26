using Controller.InGame.UserInterface;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.UserInterface.Goal;
using View.InGame.UserInterface.Normal;
using View.InGame.UserInterface.Stop;

namespace Installer.InGame.UserInterface
{
    public class UiInstaller: LifetimeScope
    {
        [SerializeField] private NormalUiView normalUiView;
        [SerializeField] private StopUiView stopUiView;
        [SerializeField] private GoalUiView goalUiView;
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            normalUiView.Register(builder);
            stopUiView.Register(builder);
            goalUiView.Register(builder);
            
            // Controller
            builder.Register<UserInterfaceState>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<UserInterfaceStateMachine>();
            builder.Register<NormalStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StopStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GoalStateController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
using Controller.InGame.UserInterface;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.UserInterface.Goal;
using View.InGame.UserInterface.Normal;
using View.InGame.UserInterface.Stop;

namespace Installer.InGame.UserInterface
{
    public class UiInstaller : LifetimeScope
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
            builder.Register<UserInterfaceState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterEntryPoint<UserInterfaceStateMachine>();
            builder.Register<NormalStateController>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<StopStateController>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<GoalStateController>(Lifetime.Transient).AsImplementedInterfaces();
        }

        private void Start()
        {
            UniTask.RunOnThreadPool((o =>
            {
                var installer = o as LifetimeScope;
                installer!.Build();
            }), this).Forget();
        }
    }
}
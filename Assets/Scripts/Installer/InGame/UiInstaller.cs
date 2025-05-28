using Adapter.Repository.InGame.Ui;
using Adapter.View.InGame.Ui;
using Adapter.View.InGame.Ui.Goal;
using Adapter.View.InGame.Ui.Normal;
using Adapter.View.InGame.Ui.Stop;
using Domain.Controller.InGame;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.InGame
{
    public class UiInstaller : LifetimeScope
    {
        [SerializeField] private NormalUi normalUi;
        [SerializeField] private StopUi stopUi;
        [SerializeField] private GoalUi goalUi;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.Register(normalUi);
            builder.Register(stopUi);
            builder.Register(goalUi);

            // Repository
            builder.Register<UiState>(Lifetime.Singleton).AsImplementedInterfaces();

            // Controller
            builder.RegisterEntryPoint<UserInterfaceStateMachine>();
            builder.Register<NormalStateController>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<StopStateController>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<GoalStateController>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}
using Adapter.Repository.InGame.Ui;
using Adapter.View.InGame.UI;
using Domain.UseCase.InGame.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.InGame
{
    public class UiInstaller : LifetimeScope
    {
        [SerializeField] private StopUi stopUi;
        [SerializeField] private HeightView heightView;
        [SerializeField] private GoalMessageView goalMessageView;
        [SerializeField] private StopButton stopButton;
        [SerializeField] private PlayButtonView playButtonView;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(heightView).AsImplementedInterfaces();
            builder.RegisterInstance(goalMessageView).AsImplementedInterfaces();
            builder.RegisterInstance(stopButton).AsImplementedInterfaces();
            builder.RegisterInstance(playButtonView).AsImplementedInterfaces();

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
using System.Collections.Generic;
using System.Linq;
using Adapter.IView.InGame.UI;
using Adapter.Presenter.InGame.UI;
using Adapter.View.InGame.UI;
using Domain.IUseCase.InGame;
using Domain.UseCase.InGame.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.InGame
{
    public class UiInstaller : LifetimeScope
    {
        [SerializeField] private List<SceneChangeButton> sceneChangeButtons;
        [SerializeField] private HeightView heightView;
        [SerializeField] private GoalMessageView goalMessageView;
        [SerializeField] private StopUiView stopUiView;
        [SerializeField] private NormalUiView normalUiView;
        [SerializeField] private StopButton stopButton;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(sceneChangeButtons.Select(x => x as ISceneChangeEventView).ToList()).AsImplementedInterfaces();
            builder.RegisterInstance(heightView).AsImplementedInterfaces();
            builder.RegisterInstance(goalMessageView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiView).AsImplementedInterfaces();
            builder.RegisterInstance(stopUiView).AsImplementedInterfaces();
            builder.RegisterInstance(stopButton).AsImplementedInterfaces();

            // Presenter
            builder.Register<StopEventPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<HeightPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<SceneChangePresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<NormalUiPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<GoalPresenter>(Lifetime.Scoped).AsImplementedInterfaces();

            // UseCase
            builder.Register<UserInterfaceState>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<UserInterfaceStateMachine>();
            builder.Register<NormalStateCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<StopStateCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<GoalStateCase>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}
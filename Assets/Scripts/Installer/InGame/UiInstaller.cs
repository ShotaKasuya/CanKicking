using System.Collections.Generic;
using System.Linq;
using Adapter.IView.InGame.UI;
using Adapter.Presenter.InGame.UI;
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
        [SerializeField] private List<SceneChangeButtonViewBase> sceneChangeButtons;
        [SerializeField] private HeightView heightView;
        [SerializeField] private GoalMessageView goalMessageView;
        // [SerializeField] private NormalUiView normalUiView;
        [SerializeField] private StopButton stopButton;
        [SerializeField] private StopUiView stopUiView;
        [SerializeField] private PlayButtonView playButtonView;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(sceneChangeButtons.Select(x => x as ISceneChangeEventView).ToList()).AsImplementedInterfaces();
            builder.RegisterInstance(heightView).AsImplementedInterfaces();
            builder.RegisterInstance(goalMessageView).AsImplementedInterfaces();
            // builder.RegisterInstance(normalUiView).AsImplementedInterfaces();
            builder.RegisterInstance(stopButton).AsImplementedInterfaces();
            builder.RegisterInstance(stopUiView).AsImplementedInterfaces();
            builder.RegisterInstance(playButtonView).AsImplementedInterfaces();

            // Presenter
            builder.Register<HeightPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<SceneChangePresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<GoalPresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            
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
using Adapter.Presenter.InGame.UI;
using Detail.View.InGame.UI;
using Domain.UseCase.InGame.Stage;
using Domain.UseCase.InGame.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.InGame
{
    public class UiInstaller: LifetimeScope
    {
        [SerializeField] private PowerImageView powerView;
        [SerializeField] private HeightView heightView;
        [SerializeField] private GoalMessageView goalMessageView;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(powerView).AsImplementedInterfaces();
            builder.RegisterInstance(heightView).AsImplementedInterfaces();
            builder.RegisterInstance(goalMessageView).AsImplementedInterfaces();
            
            // Presenter
            builder.Register<KickPowerPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<HeightPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GoalPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // UseCase
            builder.Register<ShowHeightCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GoalCase>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
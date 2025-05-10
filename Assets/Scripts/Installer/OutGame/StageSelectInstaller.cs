using Adapter.Presenter.Util;
using Adapter.View.View;
using Domain.UseCase.Util;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.OutGame
{
    public class StageSelectInstaller: LifetimeScope
    {
        [SerializeField] private MovableView movableView;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterComponent(movableView).AsImplementedInterfaces();
            
            // Presenter
            builder.Register<ScrollPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // UseCase
            builder.Register<ScrollCase>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
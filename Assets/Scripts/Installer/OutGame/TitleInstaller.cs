using Adapter.Presenter.OutGame.Title;
using Adapter.View.OutGame.Title;
using Domain.UseCase.OutGame.Title;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.OutGame
{
    public class TitleInstaller: LifetimeScope
    {
        [SerializeField] private StartButtonView startButtonView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<TitleEventPresenter>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<TitleCase>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
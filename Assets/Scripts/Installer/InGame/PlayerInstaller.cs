using Adapter.Presenter.InGame.Player;
using Adapter.Repository.InGame.Player;
using Detail.DataStore.InGame.Player;
using Detail.View.InGame.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.InGame
{
    public class PlayerInstaller : LifetimeScope
    {
        [SerializeField] private PlayerKickStatusDataStore playerStatusDataObject;

        protected override void Configure(IContainerBuilder builder)
        {
            // view
            var playerView = GetComponent<PlayerView>();
            builder.RegisterComponent(playerView);

            // Presenter
            builder.Register<PlayerKickPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerPresenter>(Lifetime.Singleton).AsImplementedInterfaces();

            // Repository
            builder.Register<PowerRepository>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerStatusRepository>(Lifetime.Singleton).AsImplementedInterfaces();

            // UseCase
        }
    }
}
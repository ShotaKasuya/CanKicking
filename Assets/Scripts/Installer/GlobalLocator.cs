using Adapter.DataStore.InGame.Player;
using Adapter.Presenter.Scene;
using Adapter.View.Scene;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer
{
    public class GlobalLocator : LifetimeScope
    {
        [SerializeField] private PlayerConstantsDataStore playerConstantsDataStore;
        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance(playerConstantsDataStore).AsImplementedInterfaces();
            
            // View
            builder.Register<SceneLoadView>(Lifetime.Singleton).AsImplementedInterfaces();

            // Presenter
            builder.Register<SceneLoadPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
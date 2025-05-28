using Adapter.DataStore.InGame.Player;
using Adapter.DataStore.Setting;
using Adapter.Repository.InGame;
using Adapter.View.Scene;
using Adapter.View.Util;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer
{
    public class GlobalLocator : LifetimeScope
    {
        [SerializeField] private PlayerConstantsDataStore playerConstantsDataStore;
        [SerializeField] private TimeScaleDataStore timeScaleDataStore;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance(playerConstantsDataStore).AsImplementedInterfaces();
            builder.RegisterInstance(timeScaleDataStore).AsImplementedInterfaces();
            
            // View
            builder.Register<SceneLoadView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<InputSystem_Actions>(Lifetime.Singleton);
            
            // Repository
            builder.Register<TimeScaleRepository>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
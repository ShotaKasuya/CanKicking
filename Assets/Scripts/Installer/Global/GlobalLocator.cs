using Model.Global;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.Global.Ads;
using View.Global.Input;
using View.Global.Scene;

namespace Installer.Global
{
    public class GlobalLocator: LifetimeScope
    {
        [SerializeField] private TimeScaleModel timeScaleModel;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputSystem_Actions>(Lifetime.Singleton);
            builder.Register<BottomAdsView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SceneLoaderView>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterInstance(timeScaleModel).AsImplementedInterfaces();
        }
    }
}
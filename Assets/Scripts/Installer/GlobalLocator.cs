using Adapter.Presenter.Scene;
using Adapter.View.InGame.Input;
using Adapter.View.Scene;
using Adapter.View.Util;
using VContainer;
using VContainer.Unity;

namespace Installer
{
    public class GlobalLocator : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputSystem_Actions>(Lifetime.Singleton);
            builder.Register<SceneLoadView>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<SceneLoadPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
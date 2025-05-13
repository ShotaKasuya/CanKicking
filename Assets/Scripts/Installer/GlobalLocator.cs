using Adapter.Presenter.Scene;
using Adapter.View.InGame.Input;
using Adapter.View.Scene;
using VContainer;
using VContainer.Unity;

namespace Installer
{
    public class GlobalLocator : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputView>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<SceneLoadView>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<SceneLoadPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
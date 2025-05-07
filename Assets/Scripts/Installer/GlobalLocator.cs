using Adapter.Presenter.Scene;
using Detail.View.InGame.Input;
using Detail.View.Scene;
using VContainer;
using VContainer.Unity;

namespace Installer
{
    public class GlobalLocator : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SceneLoadView>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<SceneLoadPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
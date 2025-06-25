using VContainer;
using VContainer.Unity;
using View.InGame.Player;

namespace Installer.Global
{
    public class GlobalLocator: LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputSystem_Actions>(Lifetime.Singleton);
        }
    }
}
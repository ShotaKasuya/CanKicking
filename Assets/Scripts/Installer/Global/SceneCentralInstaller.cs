using VContainer;
using VContainer.Unity;

namespace Installer.Global
{
    public abstract class SceneCentralInstaller: LifetimeScope
    {
        protected abstract override void Configure(IContainerBuilder builder);
    }
}
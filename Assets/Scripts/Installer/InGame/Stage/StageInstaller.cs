using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.Stage;

namespace Installer.InGame.Stage
{
    public class StageInstaller: LifetimeScope
    {
        [SerializeField] private GoalView goalView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(goalView).AsImplementedInterfaces();
            
            
        }
    }
}
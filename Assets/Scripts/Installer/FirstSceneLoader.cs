using Controller.Global;
using Model.OutGame;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer
{
    public class FirstSceneLoader : LifetimeScope
    {
        [SerializeField] private EntrySceneModel entrySceneModel;

        protected override void Configure(IContainerBuilder builder)
        {
            // Model
            builder.RegisterInstance(entrySceneModel).AsImplementedInterfaces();

            // Controller
            builder.RegisterEntryPoint<FirstSceneController>();
        }
    }
}
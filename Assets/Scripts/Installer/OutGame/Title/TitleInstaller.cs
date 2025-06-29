using Controller.OutGame;
using Model.OutGame.Title;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.OutGame.Title;

namespace Installer.OutGame.Title
{
    public class TitleInstaller: LifetimeScope
    {
        [SerializeField] private GameStartSceneModel gameStartSceneModel;
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.Register<StartGameView>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Model
            builder.RegisterInstance(gameStartSceneModel).AsImplementedInterfaces();

            // Controller
            builder.RegisterEntryPoint<TitleController>();
        }
    }
}
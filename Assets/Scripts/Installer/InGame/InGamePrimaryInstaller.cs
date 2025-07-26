using Controller.InGame;
using Installer.Global;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.Player;
using View.InGame.Stage;

namespace Installer.InGame
{
    public class InGamePrimaryInstaller: PrimarySceneInstaller
    {
        [SerializeField] private PlayerView playerView;
        
        protected override void InnerConfigure(IContainerBuilder builder)
        {
            // View
            builder.RegisterComponent(playerView).AsImplementedInterfaces();
            builder.Register<StartPositionView>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Controller
            builder.UseEntryPoints(pointsBuilder =>
            {
                pointsBuilder.Add<GameStartController>();
            });
        }
    }
}
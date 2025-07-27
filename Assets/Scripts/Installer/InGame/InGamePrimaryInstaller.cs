using Controller.InGame;
using Controller.InGame.Player;
using Installer.Global;
using Model.InGame.Stage;
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
            
            // Model
            builder.Register<GoalEventModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Controller
            builder.Register<PlayerState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.UseEntryPoints(pointsBuilder =>
            {
                pointsBuilder.Add<GameStartController>();
            });
        }
    }
}
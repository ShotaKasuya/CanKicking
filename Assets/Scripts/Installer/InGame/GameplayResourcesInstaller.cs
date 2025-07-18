using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.Player;

namespace Installer.InGame
{
    public class GameplayResourcesInstaller: LifetimeScope
    {
        [SerializeField] private PlayerView playerView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterComponentInNewPrefab(playerView, Lifetime.Singleton).AsImplementedInterfaces();
            
            // Model
            
            // Controller
        }
    }
}
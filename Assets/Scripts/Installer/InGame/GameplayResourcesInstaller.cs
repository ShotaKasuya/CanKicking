using Controller.Global.Scene;
using Interface.Global.Scene;
using TNRD;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.Player;

namespace Installer.InGame
{
    public class GameplayResourcesInstaller: LifetimeScope
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private SerializableInterface<IResourceScenesModel> sceneResourcesModel;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterComponent(playerView).AsImplementedInterfaces();
            
            // Model
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(sceneResourcesModel.Value);
            });
            
            // Controller
            builder.RegisterEntryPoint<ResourceSceneController>();
        }
    }
}
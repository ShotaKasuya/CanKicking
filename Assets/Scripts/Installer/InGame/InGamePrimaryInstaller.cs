using Controller.InGame;
using Controller.InGame.Player;
using Installer.Global;
using Model.InGame.Stage;
using VContainer;
using VContainer.Unity;
using View.InGame.Primary;

namespace Installer.InGame
{
    public class InGamePrimaryInstaller: PrimarySceneInstaller
    {
        protected override void InnerConfigure(IContainerBuilder builder)
        {
            // View
            builder.Register<LazyPlayerView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<LazyStartPositionView>(Lifetime.Singleton).AsImplementedInterfaces();
            
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
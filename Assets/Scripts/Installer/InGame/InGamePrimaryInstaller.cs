using Controller.InGame;
using Controller.InGame.Player;
using Controller.InGame.UserInterface;
using Installer.Global;
using Logic.InGame.Primary;
using Model.InGame.Player;
using Model.InGame.Primary;
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
            builder.Register<LazyStageView>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Model
            builder.Register<GoalEventModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<KickCountModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<KickPositionModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Logic
            builder.Register<GameRestartLogic>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Controller
            builder.Register<PlayerState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<UserInterfaceState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.UseEntryPoints(pointsBuilder =>
            {
                pointsBuilder.Add<GameStartController>();
                pointsBuilder.Add<AdvertiseController>();
            });
        }
    }
}
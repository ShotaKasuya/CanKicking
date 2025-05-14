using Adapter.IView.InGame.Player;
using Adapter.IView.InGame.Stage;
using Domain.IPresenter.InGame.Stage;

namespace Adapter.Presenter.InGame.Stage
{
    public class PlayerHeightPresenter: IPlayerHeightPresenter
    {
        public PlayerHeightPresenter
        (
            IPlayerView playerView,
            ISpawnPositionView spawnPositionView
        )
        {
            PlayerView = playerView;
            SpawnPositionView = spawnPositionView;
        }
        
        public float GetHeight()
        {
            return PlayerView.PlayerPose.position.y - SpawnPositionView.Position.position.y;
        }
        
        private IPlayerView PlayerView { get; }
        private ISpawnPositionView SpawnPositionView { get; }
    }
}
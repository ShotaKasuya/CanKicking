using Interface.InGame.Player;
using Interface.InGame.Primary;
using Interface.InGame.Stage;
using Module.Option;

namespace View.InGame.Primary
{
    public class LazyPlayerView : ILazyPlayerView
    {
        public LazyPlayerView()
        {
            PlayerView = new OnceCell<IPlayerView>();
        }

        public OnceCell<IPlayerView> PlayerView { get; }
    }

    public class LazyStartPositionView : ILazyStartPositionView
    {
        public LazyStartPositionView()
        {
            StartPosition = new OnceCell<ISpawnPositionView>();
        }

        public OnceCell<ISpawnPositionView> StartPosition { get; }
    }
}
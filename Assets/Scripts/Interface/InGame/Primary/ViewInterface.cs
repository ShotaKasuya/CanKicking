using Interface.InGame.Player;
using Interface.InGame.Stage;
using Module.Option;

namespace Interface.InGame.Primary;

public interface ILazyPlayerView
{
    public OnceCell<IPlayerView> PlayerView { get; }
}

public interface ILazyStartPositionView
{
    public OnceCell<ISpawnPositionView> StartPosition { get; }
}

public interface ILazyBaseHeightView
{
    public OnceCell<float> BaseHeight { get; }
}

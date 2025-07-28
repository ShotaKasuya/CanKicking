using Interface.InGame.Player;
using Interface.InGame.Primary;
using VContainer.Unity;

namespace Controller.InGame.Player;

public class InitializeController : IInitializable
{
    public InitializeController
    (
        IPlayerView playerView,
        ILazyPlayerView lazyPlayerView
    )
    {
        PlayerView = playerView;
        LazyPlayerView = lazyPlayerView;
    }

    public void Initialize()
    {
        LazyPlayerView.PlayerView.Init(PlayerView);
    }

    private IPlayerView PlayerView { get; }
    private ILazyPlayerView LazyPlayerView { get; }
}
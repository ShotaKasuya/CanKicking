using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.InGame.Primary;
using VContainer.Unity;

namespace Controller.InGame;

public class GameStartController : IAsyncStartable
{
    public GameStartController
    (
        ILazyStartPositionView lazyStartPositionView,
        ILazyPlayerView lazyPlayerView
    )
    {
        LazyStartPositionView = lazyStartPositionView;
        LazyPlayerView = lazyPlayerView;
    }

    public async UniTask StartAsync(CancellationToken cancellation = new CancellationToken())
    {
        await UniTask.WaitUntil(LazyPlayerView.PlayerView, cell => cell.IsInitialized, cancellationToken: cancellation);
        var playerView = LazyPlayerView.PlayerView.Unwrap();
        playerView.Activation(false);

        await UniTask.WaitUntil(
            LazyStartPositionView.StartPosition,
            cell => cell.IsInitialized,
            cancellationToken: cancellation
        );
        var startPosition = LazyStartPositionView.StartPosition.Unwrap();

        playerView.ModelTransform.position = startPosition.StartPosition.position;
        playerView.Activation(true);
    }

    private ILazyStartPositionView LazyStartPositionView { get; }
    private ILazyPlayerView LazyPlayerView { get; }
}
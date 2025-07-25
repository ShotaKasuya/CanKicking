using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.InGame.Player;
using Interface.InGame.Stage;
using VContainer.Unity;

namespace Controller.InGame;

public class GameStartController : IAsyncStartable
{
    public GameStartController
    (
        IStartPositionView startPositionView,
        IPlayerView playerView
    )
    {
        StartPositionView = startPositionView;
        PlayerView = playerView;
    }

    public async UniTask StartAsync(CancellationToken cancellation = new CancellationToken())
    {
        PlayerView.Activation(false);

        await UniTask.WaitUntil(StartPositionView, view => view.StartPosition.IsSome, cancellationToken: cancellation);
        var startPosition = StartPositionView.StartPosition.Unwrap();
    }

    private IStartPositionView StartPositionView { get; }
    private IPlayerView PlayerView { get; }
}
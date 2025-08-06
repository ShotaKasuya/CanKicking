using Interface.InGame.Primary;
using Interface.InGame.Stage;
using VContainer.Unity;

namespace Controller.InGame.Stage;

public class RespawnPlayerController : ITickable
{
    public RespawnPlayerController
    (
        ILazyPlayerView playerView,
        ISpawnPositionView spawnPositionView,
        IFallLineModel fallLineModel
    )
    {
        PlayerView = playerView;
        SpawnPositionView = spawnPositionView;
        FallLineModel = fallLineModel;
    }

    public void Tick()
    {
        if (!PlayerView.PlayerView.TryUnwrap(out var playerView)) return;

        var fallLine = FallLineModel.FallLine;

        if (playerView!.ModelTransform.position.y < fallLine)
        {
            playerView.ResetPosition(SpawnPositionView.StartPosition.position);
        }
    }

    private ILazyPlayerView PlayerView { get; }
    private ISpawnPositionView SpawnPositionView { get; }
    private IFallLineModel FallLineModel { get; }
}
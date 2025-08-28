using Interface.Model.InGame;
using Interface.View.InGame;
using UnityEngine;
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
            var pos = SpawnPositionView.StartPosition.position;
            var pose = new Pose(pos, Quaternion.identity);
            playerView.ResetPosition(pose);
        }
    }

    private ILazyPlayerView PlayerView { get; }
    private ISpawnPositionView SpawnPositionView { get; }
    private IFallLineModel FallLineModel { get; }
}
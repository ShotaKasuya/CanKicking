using Cysharp.Threading.Tasks;
using Interface.Global.Utility;
using Interface.InGame.Primary;
using VContainer.Unity;

namespace Controller.InGame;

public class GameStartController : IInitializable, IResetable
{
    public GameStartController
    (
        ILazyStartPositionView lazyStartPositionView,
        ILazyPlayerView lazyPlayerView,
        IBlockingOperationModel blockingOperationModel
    )
    {
        LazyStartPositionView = lazyStartPositionView;
        LazyPlayerView = lazyPlayerView;
        BlockingOperationModel = blockingOperationModel;
    }

    public void Initialize()
    {
        StartAsync().Forget();
    }

    private const string InitPlayerPosition = "Initialize Player Position";

    private async UniTask StartAsync()
    {
        using var _ = BlockingOperationModel.SpawnOperation(InitPlayerPosition);
        
        await UniTask.WaitUntil(LazyPlayerView.PlayerView, cell => cell.IsInitialized);
        await UniTask.WaitUntil(
            LazyStartPositionView.StartPosition,
            cell => cell.IsInitialized
        );

        InnerInitialize();
    }

    public void Reset()
    {
        InnerInitialize();
    }

    private void InnerInitialize()
    {
        var playerView = LazyPlayerView.PlayerView.Unwrap();
        var startPosition = LazyStartPositionView.StartPosition.Unwrap();
        
        playerView.Activation(false);

        playerView.ModelTransform.position = startPosition.StartPosition.position;
        
        playerView.Activation(true);
    }

    private ILazyStartPositionView LazyStartPositionView { get; }
    private ILazyPlayerView LazyPlayerView { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
}
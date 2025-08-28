using Cysharp.Threading.Tasks;
using Interface.Logic.InGame;
using Interface.Model.Global;
using Interface.Model.InGame;
using Interface.View.InGame;
using R3;
using VContainer.Unity;

namespace Controller.InGame;

public class GameStartController : IInitializable, IResetable
{
    public GameStartController
    (
        ILazyStartPositionView lazyStartPositionView,
        ILazyPlayerView lazyPlayerView,
        IKickCountModel jumpCountModel,
        IClearRecordModel clearRecordModel,
        IGoalEventModel goalEventModel,
        IPrimarySceneModel primarySceneModel,
        CompositeDisposable compositeDisposable
    )
    {
        LazyStartPositionView = lazyStartPositionView;
        LazyPlayerView = lazyPlayerView;
        JumpCountModel = jumpCountModel;
        ClearRecordModel = clearRecordModel;
        GoalEventModel = goalEventModel;
        PrimarySceneModel = primarySceneModel;

        CompositeDisposable = compositeDisposable;
    }

    public void Initialize()
    {
        StartAsync().Forget();
        GoalEventModel.GoalEvent
            .Subscribe(this, (_, controller) => controller.SaveRecord())
            .AddTo(CompositeDisposable);
    }

    private async UniTask StartAsync()
    {
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

    private void SaveRecord()
    {
        var record = JumpCountModel.KickCount.CurrentValue;
        var key = PrimarySceneModel.GetCurrentSceneContext.ScenePath!;

        ClearRecordModel.Save(key, record);
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ILazyStartPositionView LazyStartPositionView { get; }
    private ILazyPlayerView LazyPlayerView { get; }
    private IKickCountModel JumpCountModel { get; }
    private IClearRecordModel ClearRecordModel { get; }
    private IGoalEventModel GoalEventModel { get; }
    private IPrimarySceneModel PrimarySceneModel { get; }
}
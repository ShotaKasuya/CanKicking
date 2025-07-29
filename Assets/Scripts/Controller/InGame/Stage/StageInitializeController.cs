using Interface.InGame.Primary;
using Interface.InGame.Stage;
using R3;
using VContainer.Unity;

namespace Controller.InGame.Stage;

public class StageInitializeController : IStartable
{
    public StageInitializeController
    (
        ILazyBaseHeightView lazyBaseHeightView,
        IBaseHeightView baseHeightView,
        ILazyStartPositionView startPositionView,
        ISpawnPositionView spawnPositionView,
        IGoalEventView goalEventView,
        IGoalEventSubjectModel goalEventSubjectModel,
        CompositeDisposable compositeDisposable
    )
    {
        LazyBaseHeightView = lazyBaseHeightView;
        BaseHeightView = baseHeightView;
        StartPositionView = startPositionView;
        SpawnPositionView = spawnPositionView;
        GoalEventView = goalEventView;
        GoalEventSubjectModel = goalEventSubjectModel;
        CompositeDisposable = compositeDisposable;
    }

    public void Start()
    {
        LazyBaseHeightView.BaseHeight.Init(BaseHeightView.PositionY);
        StartPositionView.StartPosition.Init(SpawnPositionView);
        GoalEventView.Performed
            .Subscribe(this, (unit, controller) => controller.GoalEventSubjectModel.GoalEventSubject.OnNext(unit))
            .AddTo(CompositeDisposable);
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ILazyBaseHeightView LazyBaseHeightView { get; }
    private IBaseHeightView BaseHeightView { get; }
    private ILazyStartPositionView StartPositionView { get; }
    private ISpawnPositionView SpawnPositionView { get; }
    private IGoalEventView GoalEventView { get; }
    private IGoalEventSubjectModel GoalEventSubjectModel { get; }
}
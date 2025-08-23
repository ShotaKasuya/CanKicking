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
        ILazyGoalHeightView lazyGoalHeightView,
        IGoalHeightView goalHeightView,
        IGoalEventView goalEventView,
        IGoalEventSubjectModel goalEventSubjectModel,
        CompositeDisposable compositeDisposable
    )
    {
        LazyBaseHeightView = lazyBaseHeightView;
        BaseHeightView = baseHeightView;
        StartPositionView = startPositionView;
        SpawnPositionView = spawnPositionView;
        LazyGoalHeightView = lazyGoalHeightView;
        GoalHeightView = goalHeightView;
        GoalEventView = goalEventView;
        GoalEventSubjectModel = goalEventSubjectModel;
        CompositeDisposable = compositeDisposable;
    }

    public void Start()
    {
        LazyGoalHeightView.GoalHeight.Init(GoalHeightView.PositionY);
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
    private ILazyGoalHeightView LazyGoalHeightView { get; }
    private IGoalHeightView GoalHeightView { get; }
    private IGoalEventView GoalEventView { get; }
    private IGoalEventSubjectModel GoalEventSubjectModel { get; }
}
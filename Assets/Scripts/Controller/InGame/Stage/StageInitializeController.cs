using Interface.InGame.Stage;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Controller.InGame.Stage;

public class StageInitializeController: IStartable
{
    public StageInitializeController
    (
        IStartPositionView startPositionView,
        IPlayerSpawnPositionView playerSpawnPositionView,
        IGoalEventView goalEventView,
        IGoalEventSubjectModel goalEventSubjectModel,
        CompositeDisposable compositeDisposable
    )
    {
        StartPositionView = startPositionView;
        PlayerSpawnPositionView = playerSpawnPositionView;
        GoalEventView = goalEventView;
        GoalEventSubjectModel = goalEventSubjectModel;
        CompositeDisposable = compositeDisposable;
    }
    public void Start()
    {
        Debug.Log("init");
        StartPositionView.SetStartPosition(PlayerSpawnPositionView.StartPosition);
        GoalEventView.Performed
            .Subscribe(this, (unit, controller) => controller.GoalEventSubjectModel.GoalEventSubject.OnNext(unit))
            .AddTo(CompositeDisposable);
    }

    private CompositeDisposable CompositeDisposable { get; }
    private IStartPositionView StartPositionView { get; }
    private IPlayerSpawnPositionView PlayerSpawnPositionView { get; }
    private IGoalEventView GoalEventView { get; }
    private IGoalEventSubjectModel GoalEventSubjectModel { get; }
}
using Interface.Model.Global;
using Interface.Model.InGame;
using R3;
using Structure.Global;
using VContainer.Unity;

namespace Controller.InGame.Stage;

public class TutorialController: IStartable
{
    public TutorialController
    (
        IGameStateModel gameStateModel,
        IGoalEventModel goalEventModel,
        CompositeDisposable compositeDisposable
    )
    {
        GameStateModel = gameStateModel;
        CompositeDisposable = compositeDisposable;
        GoalEventModel = goalEventModel;
    }

    public void Start()
    {
        GoalEventModel.GoalEvent
            .Subscribe(this, (_, controller) => controller.OnGoal())
            .AddTo(CompositeDisposable);
    }

    private void OnGoal()
    {
        GameStateModel.UpdateGameState(GameState.Normal);
    }

    private CompositeDisposable CompositeDisposable { get; }
    private IGoalEventModel GoalEventModel { get; }
    private IGameStateModel GameStateModel { get; }
}
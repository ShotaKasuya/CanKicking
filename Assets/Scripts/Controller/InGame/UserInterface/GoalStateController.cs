using System.Threading;
using Controller.InGame.Player;
using Cysharp.Threading.Tasks;
using Interface.Logic.Global;
using Interface.Logic.InGame;
using Interface.View.InGame.UserInterface;
using R3;
using Structure.InGame.Player;
using Structure.InGame.UserInterface;
using VContainer.Unity;

namespace Controller.InGame.UserInterface;

/// <summary>
/// ゴール到達後のUIを管理する
/// </summary>
public class GoalStateController : UserInterfaceBehaviourBase, IStartable
{
    public GoalStateController
    (
        IGoalUiView goalUiView,
        IGoal_RestartButtonView restartButtonView,
        IGoal_StageSelectButtonView stageSelectButtonView,
        ILoadPrimarySceneLogic loadPrimarySceneLogic,
        IGameRestartLogic gameRestartLogic,
        CompositeDisposable compositeDisposable,
        PlayerState playerState,
        UserInterfaceState stateEntity
    ) : base(UserInterfaceStateType.Goal, stateEntity)
    {
        GoalUiView = goalUiView;
        RestartButtonView = restartButtonView;
        StageSelectButtonView = stageSelectButtonView;
        LoadPrimarySceneLogic = loadPrimarySceneLogic;
        GameRestartLogic = gameRestartLogic;
        PlayerState = playerState;
        CompositeDisposable = compositeDisposable;
    }

    public void Start()
    {
        RestartButtonView.Performed
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (_, controller) => controller.Restart())
            .AddTo(CompositeDisposable);
        StageSelectButtonView.Performed
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (sceneName, controller) => controller.Load(sceneName))
            .AddTo(CompositeDisposable);
    }

    public override async UniTask OnEnter(CancellationToken token)
    {
        await PlayerState.ChangeState(PlayerStateType.Stopping);
        await GoalUiView.Show(token);
    }

    public override UniTask OnExit(CancellationToken token)
    {
        return GoalUiView.Hide(token);
    }

    private void Load(string sceneName)
    {
        LoadPrimarySceneLogic.ChangeScene(sceneName).Forget();
    }

    private void Restart()
    {
        GameRestartLogic.RestartGame();
    }

    private PlayerState PlayerState { get; }
    private IGoalUiView GoalUiView { get; }
    private IGoal_RestartButtonView RestartButtonView { get; }
    private IGoal_StageSelectButtonView StageSelectButtonView { get; }
    private ILoadPrimarySceneLogic LoadPrimarySceneLogic { get; }
    private IGameRestartLogic GameRestartLogic { get; }
    private CompositeDisposable CompositeDisposable { get; }
}
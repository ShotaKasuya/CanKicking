using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.InGame.Primary;
using Interface.InGame.UserInterface;
using Module.StateMachine;
using R3;
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
        IMutStateEntity<UserInterfaceStateType> stateEntity
    ) : base(UserInterfaceStateType.Goal, stateEntity)
    {
        GoalUiView = goalUiView;
        RestartButtonView = restartButtonView;
        StageSelectButtonView = stageSelectButtonView;
        LoadPrimarySceneLogic = loadPrimarySceneLogic;
        GameRestartLogic = gameRestartLogic;
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
        await GoalUiView.Show(token);
    }

    private void Load(string sceneName)
    {
        LoadPrimarySceneLogic.ChangeScene(sceneName).Forget();
    }

    private void Restart()
    {
        GameRestartLogic.RestartGame();
    }

    private IGoalUiView GoalUiView { get; }
    private IGoal_RestartButtonView RestartButtonView { get; }
    private IGoal_StageSelectButtonView StageSelectButtonView { get; }
    private ILoadPrimarySceneLogic LoadPrimarySceneLogic { get; }
    private IGameRestartLogic GameRestartLogic { get; }
    private CompositeDisposable CompositeDisposable { get; }
}
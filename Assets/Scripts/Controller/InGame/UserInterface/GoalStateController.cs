using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
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
        CompositeDisposable compositeDisposable,
        IMutStateEntity<UserInterfaceStateType> stateEntity
    ) : base(UserInterfaceStateType.Goal, stateEntity)
    {
        GoalUiView = goalUiView;
        RestartButtonView = restartButtonView;
        StageSelectButtonView = stageSelectButtonView;
        LoadPrimarySceneLogic = loadPrimarySceneLogic;
        CompositeDisposable = compositeDisposable;
    }

    public void Start()
    {
        RestartButtonView.Performed
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (sceneName, controller) => controller.Load(sceneName))
            .AddTo(CompositeDisposable);
        StageSelectButtonView.Performed
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (sceneName, controller) => controller.Load(sceneName))
            .AddTo(CompositeDisposable);
    }

    public override void OnEnter()
    {
        GoalUiView.Show();
    }

    private void Load(string sceneName)
    {
        LoadPrimarySceneLogic.ChangeScene(sceneName).Forget();
    }

    private IGoalUiView GoalUiView { get; }
    private IGoal_RestartButtonView RestartButtonView { get; }
    private IGoal_StageSelectButtonView StageSelectButtonView { get; }
    private ILoadPrimarySceneLogic LoadPrimarySceneLogic { get; }
    private CompositeDisposable CompositeDisposable { get; }
}
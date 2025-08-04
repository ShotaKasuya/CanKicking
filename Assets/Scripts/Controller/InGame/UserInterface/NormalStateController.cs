using Cysharp.Threading.Tasks;
using Interface.InGame.Primary;
using Interface.InGame.Stage;
using Interface.InGame.UserInterface;
using Module.StateMachine;
using R3;
using Structure.InGame.UserInterface;
using UnityEngine;
using VContainer.Unity;

namespace Controller.InGame.UserInterface;

/// <summary>
/// 通常のUIを管理する
/// </summary>
public class NormalStateController : UserInterfaceBehaviourBase, IStartable
{
    public NormalStateController
    (
        INormalUiView normalUiView,
        ILazyBaseHeightView baseHeightView,
        ILazyGoalHeightView goalHeightView,
        IStopButtonView stopButtonView,
        IProgressUiView progressUiView,
        ILazyPlayerView playerView,
        IGoalEventModel goalEventModel,
        CompositeDisposable compositeDisposable,
        IMutStateEntity<UserInterfaceStateType> stateEntity
    ) : base(UserInterfaceStateType.Normal, stateEntity)
    {
        NormalUiView = normalUiView;
        BaseHeightView = baseHeightView;
        GoalHeightView = goalHeightView;
        StopButtonView = stopButtonView;
        GoalEventModel = goalEventModel;
        ProgressUiView = progressUiView;
        PlayerView = playerView;
        CompositeDisposable = compositeDisposable;
    }

    public void Start()
    {
        StopButtonView.Performed
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (_, controller) => controller.ChangeToStop())
            .AddTo(CompositeDisposable);
        GoalEventModel.GoalEvent
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (_, controller) => controller.ChangeToGoal())
            .AddTo(CompositeDisposable);
    }

    public override void StateUpdate(float deltaTime)
    {
        if (!PlayerView.PlayerView.TryUnwrap(out var playerView)) return;
        if (!BaseHeightView.BaseHeight.TryUnwrap(out var baseHeight)) return;
        if (!GoalHeightView.GoalHeight.TryUnwrap(out var goalHeight)) return;

        var height = playerView!.ModelTransform.position.y;
        var progress = height / (goalHeight - baseHeight);

        ProgressUiView.SetProgress(Mathf.Clamp01(progress));
    }

    private void ChangeToGoal()
    {
        StateEntity.ChangeState(UserInterfaceStateType.Goal);
    }

    private void ChangeToStop()
    {
        StateEntity.ChangeState(UserInterfaceStateType.Stop);
    }

    public override void OnEnter()
    {
        NormalUiView.Show().Forget();
    }

    public override void OnExit()
    {
        NormalUiView.Hide().Forget();
    }

    private INormalUiView NormalUiView { get; }
    private ILazyBaseHeightView BaseHeightView { get; }
    private ILazyGoalHeightView GoalHeightView { get; }
    private IStopButtonView StopButtonView { get; }
    private IGoalEventModel GoalEventModel { get; }
    private IProgressUiView ProgressUiView { get; }
    private ILazyPlayerView PlayerView { get; }
    private CompositeDisposable CompositeDisposable { get; }
}
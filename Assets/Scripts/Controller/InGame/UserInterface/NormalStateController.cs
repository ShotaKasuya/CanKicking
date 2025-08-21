using System.Threading;
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
        ILazyPlayerView playerView,
        ILazyBaseHeightView baseHeightView,
        ILazyGoalHeightView goalHeightView,
        IStopButtonView stopButtonView,
        IProgressUiView progressUiView,
        IJumpCountUiView jumpCountUiView,
        IGoalEventModel goalEventModel,
        IJumpCountModel jumpCountModel,
        CompositeDisposable compositeDisposable,
        IMutStateEntity<UserInterfaceStateType> stateEntity
    ) : base(UserInterfaceStateType.Normal, stateEntity)
    {
        NormalUiView = normalUiView;
        PlayerView = playerView;
        BaseHeightView = baseHeightView;
        GoalHeightView = goalHeightView;
        StopButtonView = stopButtonView;
        JumpCountUiView = jumpCountUiView;
        GoalEventModel = goalEventModel;
        JumpCountModel = jumpCountModel;
        ProgressUiView = progressUiView;
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
        JumpCountModel.JumpCount
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (i, controller) => controller.JumpCountUiView.SetCount(i))
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

    private const string NormalStateSequence = "NormalState";

    public override async UniTask OnEnter(CancellationToken token)
    {
        await NormalUiView.Show();
    }

    public override async UniTask OnExit(CancellationToken token)
    {
        using var handle = StateEntity.GetStateLock(NormalStateSequence);
        await NormalUiView.Hide();
    }

    private INormalUiView NormalUiView { get; }
    private ILazyPlayerView PlayerView { get; }
    private ILazyBaseHeightView BaseHeightView { get; }
    private ILazyGoalHeightView GoalHeightView { get; }
    private IStopButtonView StopButtonView { get; }
    private IJumpCountUiView JumpCountUiView { get; }
    private IGoalEventModel GoalEventModel { get; }
    private IProgressUiView ProgressUiView { get; }
    private IJumpCountModel JumpCountModel { get; }
    private CompositeDisposable CompositeDisposable { get; }
}
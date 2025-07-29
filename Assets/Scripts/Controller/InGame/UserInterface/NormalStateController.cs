using System;
using Cysharp.Threading.Tasks;
using Interface.InGame.Primary;
using Interface.InGame.Stage;
using Interface.InGame.UserInterface;
using Module.StateMachine;
using R3;
using Structure.InGame.UserInterface;
using VContainer.Unity;

namespace Controller.InGame.UserInterface;

/// <summary>
/// 通常のUIを管理する
/// </summary>
public class NormalStateController : UserInterfaceBehaviourBase, IStartable, IDisposable
{
    public NormalStateController
    (
        INormalUiView normalUiView,
        ILazyBaseHeightView baseHeightView,
        IStopButtonView stopButtonView,
        IHeightUiView heightUiView,
        ILazyPlayerView playerView,
        IGoalEventModel goalEventModel,
        IMutStateEntity<UserInterfaceStateType> stateEntity
    ) : base(UserInterfaceStateType.Normal, stateEntity)
    {
        NormalUiView = normalUiView;
        BaseHeightView = baseHeightView;
        StopButtonView = stopButtonView;
        GoalEventModel = goalEventModel;
        HeightUiView = heightUiView;
        PlayerView = playerView;
        CompositeDisposable = new CompositeDisposable();
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

        var height = playerView!.ModelTransform.position.y;
        HeightUiView.SetHeight(height - baseHeight);
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
    private IStopButtonView StopButtonView { get; }
    private IGoalEventModel GoalEventModel { get; }
    private IHeightUiView HeightUiView { get; }
    private ILazyPlayerView PlayerView { get; }
    private CompositeDisposable CompositeDisposable { get; }

    public void Dispose()
    {
        CompositeDisposable.Dispose();
    }
}
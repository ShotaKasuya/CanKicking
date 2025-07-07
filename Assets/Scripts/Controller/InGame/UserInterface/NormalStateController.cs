using System;
using Cysharp.Threading.Tasks;
using Interface.InGame.Player;
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
        IStopButtonView stopButtonView,
        IHeightUiView heightUiView,
        IPlayerView playerView,
        IGoalEventView goalEventView,
        IPullRangeView pullRangeView,
        ITouchView touchView,
        IPullLimitModel pullLimitModel,
        IMutStateEntity<UserInterfaceStateType> stateEntity
    ) : base(UserInterfaceStateType.Normal, stateEntity)
    {
        NormalUiView = normalUiView;
        StopButtonView = stopButtonView;
        GoalEventView = goalEventView;
        HeightUiView = heightUiView;
        PlayerView = playerView;
        PullRangeView = pullRangeView;
        TouchView = touchView;
        PullLimitModel = pullLimitModel;
        CompositeDisposable = new CompositeDisposable();
    }

    public void Start()
    {
        StopButtonView.Performed
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (_, controller) => controller.ChangeToStop())
            .AddTo(CompositeDisposable);
        GoalEventView.Performed
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (_, controller) => controller.ChangeToGoal())
            .AddTo(CompositeDisposable);
        TouchView.TouchEvent
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (argument, controller) => controller.OnTouch(argument))
            .AddTo(CompositeDisposable);
        TouchView.TouchEndEvent
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (_, controller) => controller.PullRangeView.HideRange())
            .AddTo(CompositeDisposable);
    }

    public override void StateUpdate(float deltaTime)
    {
        var height = PlayerView.ModelTransform.position.y;
        HeightUiView.SetHeight(height);
    }

    private void OnTouch(TouchStartEventArgument touchStartEventArgument)
    {
        var aimContext = new AimContext(
            touchStartEventArgument.TouchPosition,
            PullLimitModel.CancelRatio,
            PullLimitModel.MaxRatio
        );
        PullRangeView.ShowRange(aimContext);
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
    private IStopButtonView StopButtonView { get; }
    private IGoalEventView GoalEventView { get; }
    private IHeightUiView HeightUiView { get; }
    private IPullRangeView PullRangeView { get; }
    private IPlayerView PlayerView { get; }
    private ITouchView TouchView { get; }
    private IPullLimitModel PullLimitModel { get; }
    private CompositeDisposable CompositeDisposable { get; }

    public void Dispose()
    {
        CompositeDisposable.Dispose();
    }
}
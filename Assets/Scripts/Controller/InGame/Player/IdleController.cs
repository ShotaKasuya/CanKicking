using Interface.Model.Global;
using Interface.Model.InGame;
using Interface.View.Global;
using Interface.View.InGame;
using Module.StateMachine;
using R3;
using Structure.InGame.Player;
using Structure.Utility.Calculation;
using VContainer.Unity;

namespace Controller.InGame.Player;

public class IdleController : PlayerStateBehaviourBase, IStartable
{
    public IdleController
    (
        ITouchView touchView,
        IDoubleTapView doubleTapView,
        IPlayerView playerView,
        IRayCasterView rayCasterView,
        IGroundDetectionModel groundDetectionModel,
        IPullLimitModel pullLimitModel,
        IScreenScaleModel screenScaleModel,
        IKickPositionModel kickPositionModel,
        CompositeDisposable compositeDisposable,
        IMutStateEntity<PlayerStateType> stateEntity
    ) : base(PlayerStateType.Idle, stateEntity)
    {
        TouchView = touchView;
        DoubleTapView = doubleTapView;
        PlayerView = playerView;
        RayCasterView = rayCasterView;
        GroundDetectionModel = groundDetectionModel;
        PullLimitModel = pullLimitModel;
        ScreenScaleModel = screenScaleModel;
        KickPositionModel = kickPositionModel;
        CompositeDisposable = compositeDisposable;
    }

    public void Start()
    {
        DoubleTapView.DoubleTapEvent
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (_, controller) => controller.Undo())
            .AddTo(CompositeDisposable);
    }

    private void Undo()
    {
        if (!KickPositionModel.PopPosition().TryGetValue(out var position)) return;

        PlayerView.ResetPosition(position);
    }

    public override void StateUpdate(float deltaTime)
    {
        var isGround = !IsGrounded();
        var isAiming = IsAiming();

        if (isGround)
        {
            StateEntity.ChangeState(PlayerStateType.Frying);
            return;
        }

        if (isAiming)
        {
            StateEntity.ChangeState(PlayerStateType.Aiming);
        }
    }

    private bool IsAiming()
    {
        if (!TouchView.DraggingInfo.TryGetValue(out var info))
        {
            return false;
        }

        var screen = ScreenScaleModel.Scale;
        var (_, length) = Calculator.FitVectorToScreen(info.Delta, screen);
        var cancelLength = PullLimitModel.CancelRatio;

        var result = length > cancelLength;

        return result;
    }

    private bool IsGrounded()
    {
        var castHit = RayCasterView.PoolRay(GroundDetectionModel.GroundDetectionInfo);
        var maxSlope = GroundDetectionModel.MaxSlope;
        for (int i = 0; i < castHit.Length; i++)
        {
            var normal = castHit[i].normal;
            var slope = Calculator.NormalToSlope(normal);
            var isGround = maxSlope > slope;

            if (isGround)
            {
                return true;
            }
        }

        return false;
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ITouchView TouchView { get; }
    private IDoubleTapView DoubleTapView { get; }
    private IPlayerView PlayerView { get; }
    private IRayCasterView RayCasterView { get; }
    private IGroundDetectionModel GroundDetectionModel { get; }
    private IPullLimitModel PullLimitModel { get; }
    private IScreenScaleModel ScreenScaleModel { get; }
    private IKickPositionModel KickPositionModel { get; }
}
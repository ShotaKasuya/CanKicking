using Interface.Global.Input;
using Interface.Global.Screen;
using Interface.InGame.Player;
using Module.StateMachine;
using Structure.InGame.Player;
using Structure.Utility.Calculation;

namespace Controller.InGame.Player;

public class IdleController : PlayerStateBehaviourBase
{
    public IdleController
    (
        IRayCasterView rayCasterView,
        IGroundDetectionModel groundDetectionModel,
        ITouchView touchView,
        IPullLimitModel pullLimitModel,
        IScreenScaleModel screenScaleModel,
        IMutStateEntity<PlayerStateType> stateEntity
    ) : base(PlayerStateType.Idle, stateEntity)
    {
        RayCasterView = rayCasterView;
        GroundDetectionModel = groundDetectionModel;
        TouchView = touchView;
        PullLimitModel = pullLimitModel;
        ScreenScaleModel = screenScaleModel;
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

    private IRayCasterView RayCasterView { get; }
    private IGroundDetectionModel GroundDetectionModel { get; }
    private ITouchView TouchView { get; }
    private IPullLimitModel PullLimitModel { get; }
    private IScreenScaleModel ScreenScaleModel { get; }
}
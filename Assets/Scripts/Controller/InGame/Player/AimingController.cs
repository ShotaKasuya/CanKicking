using Interface.Global.Input;
using Interface.InGame.Player;
using Interface.InGame.Primary;
using Module.StateMachine;
using R3;
using Structure.InGame.Player;
using UnityEngine;
using VContainer.Unity;

namespace Controller.InGame.Player;

public class AimingController : PlayerStateBehaviourBase, IStartable
{
    public AimingController
    (
        ITouchView touchView,
        IPlayerView playerView,
        IAimView aimView,
        ICanKickView canKickView,
        IKickPositionModel kickPositionModel,
        IKickBasePowerModel kickBasePowerModel,
        IJumpCountModel jumpCountModel,
        ICalcKickPowerLogic calcKickPowerLogic,
        CompositeDisposable compositeDisposable,
        IMutStateEntity<PlayerStateType> stateEntity
    ) : base(PlayerStateType.Aiming, stateEntity)
    {
        TouchView = touchView;
        PlayerView = playerView;
        AimView = aimView;
        CanKickView = canKickView;
        KickPositionModel = kickPositionModel;
        KickBasePowerModel = kickBasePowerModel;
        JumpCountModel = jumpCountModel;
        CalcKickPowerLogic = calcKickPowerLogic;
        CompositeDisposable = compositeDisposable;
    }

    public void Start()
    {
        TouchView.TouchEndEvent
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (argument, controller) => controller.Jump(argument))
            .AddTo(CompositeDisposable);
    }

    public override void StateUpdate(float deltaTime)
    {
        if (!TouchView.DraggingInfo.TryGetValue(out var info))
        {
            StateEntity.ChangeState(PlayerStateType.Idle);
            return;
        }

        var aimVector = CalcKickPowerLogic.CalcKickPower(info.Delta);

        if (aimVector == Vector2.zero)
        {
            StateEntity.ChangeState(PlayerStateType.Idle);
            return;
        }

        AimView.SetAim(aimVector);
    }

    public override void OnEnter()
    {
        AimView.Show();
    }

    public override void OnExit()
    {
        AimView.Hide();
    }

    private void Jump(TouchEndEventArgument fingerReleaseInfo)
    {
        var deltaPosition = fingerReleaseInfo.Delta;
        var basePower = KickBasePowerModel.KickPower;


        var power = CalcKickPowerLogic.CalcKickPower(deltaPosition);
        var kickPower = power * basePower;

        var context = new KickContext(kickPower, Mathf.Sign(kickPower.x));
        CanKickView.Kick(context);
        StorePosition();

        StateEntity.ChangeState(PlayerStateType.Frying);
    }

    private void StorePosition()
    {
        var position = PlayerView.ModelTransform.position;

        KickPositionModel.PushPosition(position);
        JumpCountModel.Inc();
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ITouchView TouchView { get; }
    private IPlayerView PlayerView { get; }
    private IAimView AimView { get; }
    private ICanKickView CanKickView { get; }
    private IKickPositionModel KickPositionModel { get; }
    private IKickBasePowerModel KickBasePowerModel { get; }
    private IJumpCountModel JumpCountModel { get; }
    private ICalcKickPowerLogic CalcKickPowerLogic { get; }
}
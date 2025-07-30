using System;
using Interface.Global.Input;
using Interface.Global.Screen;
using Interface.InGame.Player;
using Module.StateMachine;
using R3;
using Structure.InGame.Player;
using Structure.Utility.Calculation;
using UnityEngine;
using VContainer.Unity;

namespace Controller.InGame.Player;

public class AimingController : PlayerStateBehaviourBase, IStartable, IDisposable
{
    public AimingController
    (
        ITouchView touchView,
        IPlayerView playerView,
        IAimView aimView,
        ICanKickView canKickView,
        IKickPositionModel kickPositionModel,
        IKickBasePowerModel kickBasePowerModel,
        IPullLimitModel pullLimitModel,
        IScreenScaleModel screenScaleModel,
        IMutStateEntity<PlayerStateType> stateEntity
    ) : base(PlayerStateType.Aiming, stateEntity)
    {
        TouchView = touchView;
        PlayerView = playerView;
        AimView = aimView;
        CanKickView = canKickView;
        KickPositionModel = kickPositionModel;
        KickBasePowerModel = kickBasePowerModel;
        PullLimitModel = pullLimitModel;
        ScreenScaleModel = screenScaleModel;

        CompositeDisposable = new CompositeDisposable();
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

        var screen = ScreenScaleModel.Scale;
        var (aimVector, length) = Calculator.FitVectorToScreen(info.Delta, screen);
        var cancelLength = PullLimitModel.CancelRatio;
        var maxLength = PullLimitModel.MaxRatio;

        if (length < cancelLength)
        {
            StateEntity.ChangeState(PlayerStateType.Idle);
            return;
        }

        var resizeLength = Mathf.InverseLerp(cancelLength, maxLength, length);
        aimVector *= resizeLength;

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
        var screen = ScreenScaleModel.Scale;
        var cancelLength = PullLimitModel.CancelRatio;
        var maxLength = PullLimitModel.MaxRatio;
        var basePower = KickBasePowerModel.BasePower;
        
        var (aimVector, length) = Calculator.FitVectorToScreen(deltaPosition, screen);
        var resizeLength = Mathf.InverseLerp(cancelLength, maxLength, length);
        var power = basePower * resizeLength * aimVector;

        var context = new KickContext(power, Mathf.Sign(power.x));
        CanKickView.Kick(context);
        StorePosition();
        StateEntity.ChangeState(PlayerStateType.Frying);
    }

    private void StorePosition()
    {
        var position = PlayerView.ModelTransform.position;
        
        KickPositionModel.PushPosition(position);
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ITouchView TouchView { get; }
    private IPlayerView PlayerView { get; }
    private IAimView AimView { get; }
    private ICanKickView CanKickView { get; }
    private IKickPositionModel KickPositionModel { get; }
    private IKickBasePowerModel KickBasePowerModel { get; }
    private IPullLimitModel PullLimitModel { get; }
    private IScreenScaleModel ScreenScaleModel { get; }

    public void Dispose()
    {
        CompositeDisposable.Dispose();
    }
}
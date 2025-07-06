using System;
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
        IAimView aimView,
        ICanKickView canKickView,
        IKickBasePowerModel kickBasePowerModel,
        IPullLimitModel pullLimitModel,
        IMutStateEntity<PlayerStateType> stateEntity
    ) : base(PlayerStateType.Aiming, stateEntity)
    {
        TouchView = touchView;
        AimView = aimView;
        CanKickView = canKickView;
        KickBasePowerModel = kickBasePowerModel;
        PullLimitModel = pullLimitModel;

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

        var ratio = PullLimitModel.LimitRatio;
        var aimVector = Calculator.FitVectorToScreen(info.Delta * ratio);

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
        var basePower = KickBasePowerModel.BasePower;
        var deltaPosition = fingerReleaseInfo.Delta;
        var ratio = PullLimitModel.LimitRatio;
        deltaPosition = Calculator.FitVectorToScreen(deltaPosition * ratio);

        var power = deltaPosition * basePower;

        var context = new KickContext(power, Mathf.Sign(power.x));
        CanKickView.Kick(context);
        StateEntity.ChangeState(PlayerStateType.Frying);
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ITouchView TouchView { get; }
    private IAimView AimView { get; }
    private ICanKickView CanKickView { get; }
    private IKickBasePowerModel KickBasePowerModel { get; }
    private IPullLimitModel PullLimitModel { get; }

    public void Dispose()
    {
        CompositeDisposable.Dispose();
    }
}
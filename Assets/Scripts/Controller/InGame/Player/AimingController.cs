using Interface.Logic.InGame;
using Interface.Model.InGame;
using Interface.View.Global;
using Interface.View.InGame;
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
        ISeSourceView seSourceView,
        IKickPositionModel kickPositionModel,
        IKickBasePowerModel kickBasePowerModel,
        IKickCountModel jumpCountModel,
        IPlayerSoundModel playerSoundModel,
        ICalcKickPowerLogic calcKickPowerLogic,
        CompositeDisposable compositeDisposable,
        IMutStateType<PlayerStateType> innerState
    ) : base(PlayerStateType.Aiming, innerState)
    {
        TouchView = touchView;
        PlayerView = playerView;
        AimView = aimView;
        CanKickView = canKickView;
        SeSourceView = seSourceView;
        KickPositionModel = kickPositionModel;
        KickBasePowerModel = kickBasePowerModel;
        JumpCountModel = jumpCountModel;
        PlayerSoundModel = playerSoundModel;
        CalcKickPowerLogic = calcKickPowerLogic;
        CompositeDisposable = compositeDisposable;
    }

    public void Start()
    {
        TouchView.TouchEndEvent
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (argument, controller) => controller.Kick(argument))
            .AddTo(CompositeDisposable);
    }

    public override void StateUpdate(float deltaTime)
    {
        if (!TouchView.DraggingInfo.TryGetValue(out var info))
        {
            InnerState.ChangeState(PlayerStateType.Idle);
            return;
        }

        var aimVector = CalcKickPowerLogic.CalcKickPower(info.Delta);

        if (aimVector == Vector2.zero)
        {
            InnerState.ChangeState(PlayerStateType.Idle);
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

    private void Kick(TouchEndEventArgument fingerReleaseInfo)
    {
        var deltaPosition = fingerReleaseInfo.Delta;
        var basePower = KickBasePowerModel.KickPower;

        var power = CalcKickPowerLogic.CalcKickPower(deltaPosition);
        var kickPower = power * basePower;

        var context = new KickContext(kickPower, Mathf.Sign(kickPower.x));
        CanKickView.Kick(context);
        OnKick();

        InnerState.ChangeState(PlayerStateType.Frying);
    }

    private void OnKick()
    {
        // Play Se
        var clip = PlayerSoundModel.GetKickSound();
        SeSourceView.Play(clip);

        // Store Position
        var transform = PlayerView.ModelTransform;
        var position = transform.position;
        var rotation = transform.rotation;

        KickPositionModel.PushPosition(new Pose(position, rotation));
        JumpCountModel.Inc();
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ITouchView TouchView { get; }
    private IPlayerView PlayerView { get; }
    private IAimView AimView { get; }
    private ICanKickView CanKickView { get; }
    private ISeSourceView SeSourceView { get; }
    private IKickPositionModel KickPositionModel { get; }
    private IKickBasePowerModel KickBasePowerModel { get; }
    private IKickCountModel JumpCountModel { get; }
    private IPlayerSoundModel PlayerSoundModel { get; }
    private ICalcKickPowerLogic CalcKickPowerLogic { get; }
}
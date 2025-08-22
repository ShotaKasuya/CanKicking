using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.Global;
using Interface.Global.Advertisement;
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
        ISeSourceView seSourceView,
        IKickPositionModel kickPositionModel,
        IKickBasePowerModel kickBasePowerModel,
        IJumpCountModel jumpCountModel,
        IPlayerSoundModel playerSoundModel,
        ICalcKickPowerLogic calcKickPowerLogic,
        CompositeDisposable compositeDisposable,
        IMutStateEntity<PlayerStateType> stateEntity
    ) : base(PlayerStateType.Aiming, stateEntity)
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

    public override UniTask OnEnter(CancellationToken token)
    {
        AimView.Show();
        return UniTask.CompletedTask;
    }

    public override UniTask OnExit(CancellationToken token)
    {
        AimView.Hide();
        return UniTask.CompletedTask;
    }

    private void Jump(TouchEndEventArgument fingerReleaseInfo)
    {
        var deltaPosition = fingerReleaseInfo.Delta;
        var basePower = KickBasePowerModel.KickPower;

        var power = CalcKickPowerLogic.CalcKickPower(deltaPosition);
        var kickPower = power * basePower;

        var context = new KickContext(kickPower, Mathf.Sign(kickPower.x));
        CanKickView.Kick(context);
        OnJump();

        StateEntity.ChangeState(PlayerStateType.Frying);
    }

    private void OnJump()
    {
        // Play Se
        var clip = PlayerSoundModel.GetKickSound();
        SeSourceView.Play(clip);

        // Store Position
        var position = PlayerView.ModelTransform.position;

        KickPositionModel.PushPosition(position);
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
    private IJumpCountModel JumpCountModel { get; }
    private IPlayerSoundModel PlayerSoundModel { get; }
    private ICalcKickPowerLogic CalcKickPowerLogic { get; }
}
using System.Runtime.CompilerServices;
using Interface.InGame.Player;
using Module.StateMachine;
using Structure.InGame.Player;
using UnityEngine;

namespace Domain.UseCase.InGame.Player
{
    public class AimingController : PlayerStateBehaviourBase
    {
        // public AimingController
        // (
        //     ITouchView touchView,
        //     IAimView aimView,
        //     IKickPresenter kickPresenter,
        //     IKickBasePowerRepository kickBasePowerRepository,
        //     ICalcPowerEntity calcPowerEntity,
        //     IConvertScreenBaseVectorEntity convertScreenBaseVectorEntity,
        //     IMutStateEntity<PlayerStateType> stateEntity
        // ) : base(PlayerStateType.Aiming, stateEntity)
        // {
        //     DragFingerPresenter = dragFingerPresenter;
        //     FingerReleasePresenter = fingerReleasePresenter;
        //     AimPresenter = aimPresenter;
        //     KickPresenter = kickPresenter;
        //     KickBasePowerRepository = kickBasePowerRepository;
        //     CalcPowerEntity = calcPowerEntity;
        //     ConvertScreenBaseVectorEntity = convertScreenBaseVectorEntity;
        // }
        //
        // public override void StateUpdate(float deltaTime)
        // {
        //     var dragInfo = DragFingerPresenter.DragInfo.Unwrap();
        //     var aimVector = dragInfo.TouchCurrentPosition - dragInfo.TouchStartPosition;
        //     var startPosition = ToScreenBaseVector(aimVector);
        //
        //     AimPresenter.PresentAim(new AimInfo(startPosition));
        // }
        //
        // private void Jump(FingerReleaseInfo fingerReleaseInfo)
        // {
        //     var startPosition = fingerReleaseInfo.TouchStartPosition;
        //     var endPosition = fingerReleaseInfo.TouchEndPosition;
        //     var deltaPosition = ToScreenBaseVector(startPosition - endPosition);
        //     var basePower = KickBasePowerRepository.KickBasePower;
        //
        //     var power = CalcPowerEntity.CalcPower(new CalcPowerParams(
        //         deltaPosition, basePower
        //     ));
        //
        //     var arg = new KickArg(power, Mathf.Sign(power.x));
        //     KickPresenter.Kick(arg);
        //     AimPresenter.PresentAim(new AimInfo(Vector2.zero));
        //     StateEntity.ChangeState(PlayerStateType.Frying);
        // }
        //
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // private Vector2 ToScreenBaseVector(Vector2 vector2)
        // {
        //     const float ratio = 1.5f;
        //     var arg = new ScreenVectorParams(vector2, ratio, Screen.width, Screen.height);
        //     var result = ConvertScreenBaseVectorEntity.Convert(arg);
        //
        //     return result;
        // }
        //
        // public override void OnEnter()
        // {
        //     FingerReleasePresenter.OnRelease += Jump;
        // }
        //
        // public override void OnExit()
        // {
        //     FingerReleasePresenter.OnRelease -= Jump;
        // }
        //
        // private ITouchView TouchView { get; }
        // private IKickPresenter KickPresenter { get; }
        // private IAimView AimView { get; }
        // private IKickBasePowerRepository KickBasePowerRepository { get; }
        // private ICalcPowerEntity CalcPowerEntity { get; }
        // private IConvertScreenBaseVectorEntity ConvertScreenBaseVectorEntity { get; }
        public AimingController(PlayerStateType playerStateType, IMutStateEntity<PlayerStateType> stateEntity) : base(playerStateType, stateEntity)
        {
        }
    }
}
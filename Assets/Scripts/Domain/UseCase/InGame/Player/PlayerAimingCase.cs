using System.Runtime.CompilerServices;
using Domain.IEntity.InGame.Player;
using Domain.IPresenter.InGame.Player;
using Domain.IPresenter.Util;
using Domain.IRepository.InGame.Player;
using Domain.IUseCase.InGame;
using Module.StateMachine;
using Structure.InGame.Player;
using Structure.Util.Input;
using UnityEngine;

namespace Domain.UseCase.InGame.Player
{
    public class PlayerAimingCase : PlayerStateBehaviourBase
    {
        public PlayerAimingCase
        (
            IDragFingerPresenter dragFingerPresenter,
            IFingerReleasePresenter fingerReleasePresenter,
            IAimPresenter aimPresenter,
            IKickPresenter kickPresenter,
            IKickBasePowerRepository kickBasePowerRepository,
            ICalcPowerEntity calcPowerEntity,
            IMutStateEntity<PlayerStateType> stateEntity
        ) : base(PlayerStateType.Aiming, stateEntity)
        {
            DragFingerPresenter = dragFingerPresenter;
            FingerReleasePresenter = fingerReleasePresenter;
            AimPresenter = aimPresenter;
            KickPresenter = kickPresenter;
            KickBasePowerRepository = kickBasePowerRepository;
            CalcPowerEntity = calcPowerEntity;
        }

        public override void StateUpdate(float deltaTime)
        {
            var dragInfo = DragFingerPresenter.DragInfo.Unwrap();
            var aimVector = dragInfo.TouchCurrentPosition - dragInfo.TouchStartPosition;
            var startPosition = ToScreenBaseVector(aimVector);

            AimPresenter.PresentAim(new AimInfo(startPosition));
        }

        private void Jump(FingerReleaseInfo fingerReleaseInfo)
        {
            var startPosition = fingerReleaseInfo.TouchStartPosition;
            var endPosition = fingerReleaseInfo.TouchEndPosition;
            var deltaPosition = ToScreenBaseVector(startPosition - endPosition);
            var basePower = KickBasePowerRepository.KickBasePower;

            var power = CalcPowerEntity.CalcPower(new CalcPowerParams(
                deltaPosition, basePower
            ));

            var arg = new KickArg(power, Mathf.Sign(power.x));
            KickPresenter.Kick(arg);
            AimPresenter.PresentAim(new AimInfo(Vector2.zero));
            StateEntity.ChangeState(PlayerStateType.Frying);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector2 ToScreenBaseVector(Vector2 vector2)
        {
            const float ratio = 1.5f;
            var shorter = Screen.width > Screen.height ? Screen.height : Screen.width;
            var magnification = ratio / shorter;

            var x = Mathf.Clamp(vector2.x * magnification, -1, 1);
            var y = Mathf.Clamp(vector2.y * magnification, -1, 1);

            return new Vector2(x, y);
        }

        public override void OnEnter()
        {
            FingerReleasePresenter.OnRelease += Jump;
        }

        public override void OnExit()
        {
            FingerReleasePresenter.OnRelease -= Jump;
        }

        private IDragFingerPresenter DragFingerPresenter { get; }
        private IFingerReleasePresenter FingerReleasePresenter { get; }
        private IKickPresenter KickPresenter { get; }
        private IAimPresenter AimPresenter { get; }
        private IKickBasePowerRepository KickBasePowerRepository { get; }
        private ICalcPowerEntity CalcPowerEntity { get; }
    }
}
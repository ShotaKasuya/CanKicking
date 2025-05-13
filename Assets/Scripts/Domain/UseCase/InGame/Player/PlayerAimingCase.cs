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
            var width = Screen.width;
            var height = Screen.height;
            var startPosition = new Vector2(aimVector.x / width, aimVector.y / height);

            AimPresenter.PresentAim(new AimInfo(startPosition));
        }

        private void Jump(FingerReleaseInfo fingerReleaseInfo)
        {
            var width = Screen.width;
            var height = Screen.height;
            var startPosition = fingerReleaseInfo.TouchStartPosition;
            var endPosition = fingerReleaseInfo.TouchEndPosition;
            var basePower = KickBasePowerRepository.KickBasePower;

            var power = CalcPowerEntity.CalcPower(new CalcPowerParams(
                new Vector2(startPosition.x / width, startPosition.y / height),
                new Vector2(endPosition.x / width, endPosition.y / height), basePower
            ));

            var arg = new KickArg(power, Mathf.Sign(power.x));
            KickPresenter.Kick(arg);
            AimPresenter.PresentAim(new AimInfo(Vector2.zero));
            StateEntity.ChangeState(PlayerStateType.Frying);
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
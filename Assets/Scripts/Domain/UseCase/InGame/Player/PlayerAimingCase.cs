using Domain.IEntity.InGame.Player;
using Domain.IPresenter.InGame.Player;
using Domain.IPresenter.Util;
using Domain.IRepository.InGame.Player;
using Domain.IUseCase.InGame;
using Module.StateMachine;
using Structure.InGame.Player;
using Structure.Util.Input;

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
            
            AimPresenter.PresentAim(new AimInfo(aimVector.normalized));
        }

        public override void OnEnter()
        {
            FingerReleasePresenter.OnRelease += Jump;
        }

        public override void OnExit()
        {
            FingerReleasePresenter.OnRelease -= Jump;
        }

        private void Jump(FingerReleaseInfo fingerReleaseInfo)
        {
            var basePower = KickBasePowerRepository.KickBasePower;
            var power = CalcPowerEntity.CalcPower(new CalcPowerArg(
                fingerReleaseInfo.TouchStartPosition,
                fingerReleaseInfo.TouchEndPosition, basePower
            ));

            var arg = new KickArg(power, 1);
            KickPresenter.Kick(arg);
        }

        private IDragFingerPresenter DragFingerPresenter { get; }
        private IFingerReleasePresenter FingerReleasePresenter { get; }
        private IKickPresenter KickPresenter { get; }
        private IAimPresenter AimPresenter { get; }
        private IKickBasePowerRepository KickBasePowerRepository { get; }
        private ICalcPowerEntity CalcPowerEntity { get; }
    }
}
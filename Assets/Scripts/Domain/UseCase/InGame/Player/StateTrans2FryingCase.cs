using DataUtil.InGame.Player;
using Domain.IPresenter.InGame.Player;
using Domain.IRepository.InGame.Player;
using Domain.IUseCase.InGame;

namespace Domain.UseCase.InGame.Player
{
    public class StateTrans2FryingCase : PlayerStateBehaviourBase
    {
        public StateTrans2FryingCase
        (
            PlayerStateEntity playerStateEntity,
            IPlayerSpeedPresenter speedPresenter,
            IKickableSpeedRepository kickableSpeedRepository
        ) : base(playerStateEntity)
        {
            SpeedPresenter = speedPresenter;
            KickableSpeedRepository = kickableSpeedRepository;
        }

        public override void StateUpdate(float deltaTime)
        {
            var speed = SpeedPresenter.SqrSpeed();
            var movableSpeed = KickableSpeedRepository.SqrKickableVelocity;

            if (speed < movableSpeed)
            {
                StateEntity.ChangeState(PlayerStateType.Frying);
            }
        }

        public override PlayerStateType TargetStateMask => PlayerStateType.Charging | PlayerStateType.Idle;
        private IPlayerSpeedPresenter SpeedPresenter { get; }
        private IKickableSpeedRepository KickableSpeedRepository { get; }
    }
}
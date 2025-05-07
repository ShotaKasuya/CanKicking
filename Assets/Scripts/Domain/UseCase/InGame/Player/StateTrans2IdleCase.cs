using DataUtil.InGame.Player;
using Domain.IPresenter.InGame.Player;
using Domain.IRepository.InGame.Player;
using Domain.IUseCase.InGame;

namespace Domain.UseCase.InGame.Player
{
    public class StateTrans2IdleCase : PlayerStateBehaviourBase
    {
        public StateTrans2IdleCase
        (
            PlayerStateEntity playerStateEntity,
            IPlayerSpeedPresenter playerSpeedPresenter,
            IKickableSpeedRepository kickableSpeedRepository
        ) : base(PlayerStateType.Frying, playerStateEntity)
        {
            SpeedPresenter = playerSpeedPresenter;
            KickableSpeedRepository = kickableSpeedRepository;
        }

        public override void StateUpdate(float deltaTime)
        {
            var speed = SpeedPresenter.SqrSpeed();
            var movableSpeed = KickableSpeedRepository.SqrKickableVelocity;
            if (speed < movableSpeed)
            {
                StateEntity.ChangeState(PlayerStateType.Idle);
            }
        }

        private IPlayerSpeedPresenter SpeedPresenter { get; }
        private IKickableSpeedRepository KickableSpeedRepository { get; }
    }
}
using Domain.IEntity.InGame.Player;
using Domain.IPresenter.InGame.Player;
using Domain.IRepository.InGame.Player;
using Domain.IUseCase.InGame;
using Module.StateMachine;
using Structure.InGame.Player;

namespace Domain.UseCase.InGame.Player
{
    public class PlayerFryingCase : PlayerStateBehaviourBase
    {
        public PlayerFryingCase
        (
            IPlayerGroundPresenter playerGroundPresenter,
            IPlayerVelocityPresenter velocityPresenter,
            IGroundingInfoRepository groundingInfoRepository,
            IIsGroundedEntity isGroundedEntity,
            IMutStateEntity<PlayerStateType> stateEntity
        ) : base(PlayerStateType.Frying, stateEntity)
        {
            PlayerGroundPresenter = playerGroundPresenter;
            VelocityPresenter = velocityPresenter;
            GroundingInfoRepository = groundingInfoRepository;
            IsGroundedEntity = isGroundedEntity;
        }

        public override void StateUpdate(float deltaTime)
        {
            var isGround = IsGroundedEntity.IsGround(new CheckGroundParams(
                PlayerGroundPresenter.PoolGround(),
                GroundingInfoRepository.MaxSlope,
                VelocityPresenter.LinearVelocity()
            ));

            if (isGround)
            {
                StateEntity.ChangeState(PlayerStateType.Idle);
            }
        }

        private IPlayerGroundPresenter PlayerGroundPresenter { get; }
        private IPlayerVelocityPresenter VelocityPresenter { get; }
        private IGroundingInfoRepository GroundingInfoRepository { get; }
        private IIsGroundedEntity IsGroundedEntity { get; }
    }
}
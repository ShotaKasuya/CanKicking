using Domain.IEntity.InGame.Player;
using Domain.IPresenter.InGame.Player;
using Domain.IRepository.InGame.Player;
using Domain.IUseCase.InGame;
using Module.StateMachine;
using Structure.InGame.Player;
using UnityEngine;

namespace Domain.UseCase.InGame.Player
{
    public class PlayerFryingCase : PlayerStateBehaviourBase
    {
        public PlayerFryingCase
        (
            IPlayerVelocityPresenter playerVelocityPresenter,
            IPlayerContactPresenter playerContactPresenter,
            IGroundingInfoRepository groundingInfoRepository,
            IIsGroundedEntity isGroundedEntity,
            IIsStopedEntity isStopedEntity,
            IMutStateEntity<PlayerStateType> stateEntity
        ) : base(PlayerStateType.Frying, stateEntity)
        {
            VelocityPresenter = playerVelocityPresenter;
            PlayerContactPresenter = playerContactPresenter;
            GroundingInfoRepository = groundingInfoRepository;
            IsGroundedEntity = isGroundedEntity;
            IsStopedEntity = isStopedEntity;
        }

        public override void OnEnter()
        {
            PlayerContactPresenter.OnCollision += CheckGrounded;
        }

        public override void OnExit()
        {
            PlayerContactPresenter.OnCollision -= CheckGrounded;
        }

        private void CheckGrounded(Collision2D collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                var normal = collision.contacts[i].normal;

                var isGround = IsGroundedEntity.IsGround(new CheckGroundParams(
                    normal,
                    GroundingInfoRepository.MaxSlope
                ));
                if (isGround)
                {
                    StateEntity.ChangeState(PlayerStateType.Idle);
                    return;
                }
            }
        }

        public override void StateUpdate(float deltaTime)
        {
            if (IsStopedEntity.IsStop(VelocityPresenter.LinearVelocity(), VelocityPresenter.AnglerVelocity()))
            {
                StateEntity.ChangeState(PlayerStateType.Idle);
            }
        }

        private IPlayerVelocityPresenter VelocityPresenter { get; }
        private IPlayerContactPresenter PlayerContactPresenter { get; }
        private IGroundingInfoRepository GroundingInfoRepository { get; }
        private IIsGroundedEntity IsGroundedEntity { get; }
        private IIsStopedEntity IsStopedEntity { get; }
    }
}
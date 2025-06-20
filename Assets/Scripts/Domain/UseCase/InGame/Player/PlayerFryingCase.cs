using Domain.IEntity.InGame.Player;
using Domain.IPresenter.InGame.Player;
using Domain.IRepository.InGame;
using Domain.IRepository.InGame.Player;
using Module.StateMachine;
using Structure.InGame.Player;
using UnityEngine;

namespace Domain.UseCase.InGame.Player
{
    public class PlayerFryingCase : PlayerStateBehaviourBase
    {
        public PlayerFryingCase
        (
            IPlayerPresenter playerPresenter,
            IPlayerContactPresenter playerContactPresenter,
            IGroundingInfoRepository groundingInfoRepository,
            ITimeScaleRepository timeScaleRepository,
            IIsGroundedEntity isGroundedEntity,
            IIsStopedEntity isStopedEntity,
            IMutStateEntity<PlayerStateType> stateEntity
        ) : base(PlayerStateType.Frying, stateEntity)
        {
            PlayerPresenter = playerPresenter;
            PlayerContactPresenter = playerContactPresenter;
            GroundingInfoRepository = groundingInfoRepository;
            TimeScaleRepository = timeScaleRepository;
            IsGroundedEntity = isGroundedEntity;
            IsStopedEntity = isStopedEntity;
        }

        public override void OnEnter()
        {
            Time.timeScale = TimeScaleRepository.FryState;
            PlayerContactPresenter.OnCollision += CheckGrounded;
        }

        public override void OnExit()
        {
            Time.timeScale = ITimeScaleRepository.Normal;
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
            if (IsStopedEntity.IsStop(PlayerPresenter.LinearVelocity(), PlayerPresenter.AnglerVelocity()))
            {
                StateEntity.ChangeState(PlayerStateType.Idle);
            }
        }

        private IPlayerPresenter PlayerPresenter { get; }
        private IPlayerContactPresenter PlayerContactPresenter { get; }
        private IGroundingInfoRepository GroundingInfoRepository { get; }
        private ITimeScaleRepository TimeScaleRepository { get; }
        private IIsGroundedEntity IsGroundedEntity { get; }
        private IIsStopedEntity IsStopedEntity { get; }
    }
}
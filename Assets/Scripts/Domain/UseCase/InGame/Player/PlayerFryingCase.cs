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
            IRotationStopPresenter rotationStopPresenter,
            IPlayerContactPresenter playerContactPresenter,
            IRotationStateRepository rotationStateRepository,
            IGroundingInfoRepository groundingInfoRepository,
            ITimeScaleRepository timeScaleRepository,
            IIsGroundedEntity isGroundedEntity,
            IIsStopedEntity isStopedEntity,
            IMutStateEntity<PlayerStateType> stateEntity
        ) : base(PlayerStateType.Frying, stateEntity)
        {
            PlayerPresenter = playerPresenter;
            RotationStopPresenter = rotationStopPresenter;
            PlayerContactPresenter = playerContactPresenter;
            RotationStateRepository = rotationStateRepository;
            GroundingInfoRepository = groundingInfoRepository;
            TimeScaleRepository = timeScaleRepository;
            IsGroundedEntity = isGroundedEntity;
            IsStopedEntity = isStopedEntity;
        }

        public override void OnEnter()
        {
            Time.timeScale = TimeScaleRepository.FryState;
            RotationStopPresenter.Stop();
            PlayerContactPresenter.OnCollision += CheckGrounded;
        }

        public override void OnExit()
        {
            Time.timeScale = ITimeScaleRepository.Normal;
            RotationStopPresenter.ReStart();
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
            PlayerPresenter.Rotate(RotationStateRepository.RotationAngle);
            if (IsStopedEntity.IsStop(PlayerPresenter.LinearVelocity(), PlayerPresenter.AnglerVelocity()))
            {
                StateEntity.ChangeState(PlayerStateType.Idle);
            }
        }

        private IPlayerPresenter PlayerPresenter { get; }
        private IRotationStopPresenter RotationStopPresenter { get; }
        private IPlayerContactPresenter PlayerContactPresenter { get; }
        private IGroundingInfoRepository GroundingInfoRepository { get; }
        private IRotationStateRepository RotationStateRepository { get; }
        private ITimeScaleRepository TimeScaleRepository { get; }
        private IIsGroundedEntity IsGroundedEntity { get; }
        private IIsStopedEntity IsStopedEntity { get; }
    }
}
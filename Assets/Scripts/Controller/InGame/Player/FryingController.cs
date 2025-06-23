using Interface.InGame.Player;
using Module.StateMachine;
using R3;
using Structure.InGame.Player;
using Structure.Utility.Calculation;
using UnityEngine;
using VContainer.Unity;

namespace Domain.UseCase.InGame.Player
{
    public class FryingController : PlayerStateBehaviourBase, IStartable
    {
        public FryingController
        (
            IPlayerView playerView,
            IGroundDetectionModel groundDetectionModel,
            IMutStateEntity<PlayerStateType> stateEntity
        ) : base(PlayerStateType.Frying, stateEntity)
        {
            PlayerView = playerView;
            GroundDetectionModel = groundDetectionModel;

            CompositeDisposable = new CompositeDisposable();
        }
        
        public void Start()
        {
            PlayerView.CollisionEnterEvent
                .Where(_ => IsInState())
                .Subscribe(CheckGrounded)
                .AddTo(CompositeDisposable);
        }
        
        private void CheckGrounded(Collision2D collision)
        {
            var maxSlope = GroundDetectionModel.MaxSlope;
            for (int i = 0; i < collision.contactCount; i++)
            {
                var normal = collision.contacts[i].normal;
                var slope = Calculator.NormalToSlope(normal);
                var isGround = maxSlope > slope;
                
                if (isGround)
                {
                    StateEntity.ChangeState(PlayerStateType.Idle);
                    return;
                }
            }
        }
        
        private CompositeDisposable CompositeDisposable { get; }
        private IPlayerView PlayerView { get; }
        private IGroundDetectionModel GroundDetectionModel { get; }
    }
}
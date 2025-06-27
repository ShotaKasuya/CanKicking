using Interface.Global.TimeScale;
using Interface.InGame.Player;
using Module.StateMachine;
using R3;
using Structure.Global.TimeScale;
using Structure.InGame.Player;
using Structure.Utility.Calculation;
using UnityEngine;
using VContainer.Unity;

namespace Controller.InGame.Player
{
    public class FryingController : PlayerStateBehaviourBase, IStartable
    {
        public FryingController
        (
            IPlayerView playerView,
            IRayCasterView rayCasterView,
            IGroundDetectionModel groundDetectionModel,
            ITimeScaleModel timeScaleModel,
            IMutStateEntity<PlayerStateType> stateEntity
        ) : base(PlayerStateType.Frying, stateEntity)
        {
            PlayerView = playerView;
            RayCasterView = rayCasterView;
            GroundDetectionModel = groundDetectionModel;
            TimeScaleModel = timeScaleModel;

            CompositeDisposable = new CompositeDisposable();
        }

        public void Start()
        {
            PlayerView.CollisionEnterEvent
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (collision2D, controller) => controller.CheckGrounded(collision2D))
                .AddTo(CompositeDisposable);
        }

        private void CheckGrounded(Collision2D collision)
        {
            var upVelocity = PlayerView.LinearVelocity.y;
            if (upVelocity > 0f) return;

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

        public override void StateUpdate(float deltaTime)
        {
            var upVelocity = PlayerView.LinearVelocity.y;
            if (upVelocity > 0f) return;

            var castHit = RayCasterView.PoolRay(GroundDetectionModel.GroundDetectionInfo);
            var maxSlope = GroundDetectionModel.MaxSlope;
            for (int i = 0; i < castHit.Length; i++)
            {
                var normal = castHit[i].normal;
                var slope = Calculator.NormalToSlope(normal);
                var isGround = maxSlope > slope;

                if (isGround)
                {
                    StateEntity.ChangeState(PlayerStateType.Idle);
                    return;
                }
            }
        }

        public override void OnEnter()
        {
            TimeScaleModel.Execute(TimeCommandType.Frying);
        }

        public override void OnExit()
        {
            TimeScaleModel.Undo();
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IPlayerView PlayerView { get; }
        private IRayCasterView RayCasterView { get; }
        private IGroundDetectionModel GroundDetectionModel { get; }
        private ITimeScaleModel TimeScaleModel { get; }
    }
}
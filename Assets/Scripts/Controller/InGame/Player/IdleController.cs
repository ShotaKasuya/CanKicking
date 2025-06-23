using Interface.InGame.Player;
using Module.StateMachine;
using R3;
using Structure.InGame.Player;
using Structure.Utility.Calculation;
using VContainer.Unity;

namespace Domain.UseCase.InGame.Player
{
    public class IdleController : PlayerStateBehaviourBase, IStartable
    {
        public IdleController
        (
            IRayCasterView rayCasterView,
            IGroundDetectionModel groundDetectionModel,
            ITouchView touchView,
            IMutStateEntity<PlayerStateType> stateEntity
        ) : base(PlayerStateType.Idle, stateEntity)
        {
            RayCasterView = rayCasterView;
            GroundDetectionModel = groundDetectionModel;
            TouchView = touchView;

            CompositeDisposable = new CompositeDisposable();
        }
        
        public void Start()
        {
            TouchView.TouchEvent
                .Where(_ => IsInState())
                .Subscribe(ToCharge)
                .AddTo(CompositeDisposable);
        }

        public override void StateUpdate(float deltaTime)
        {
            var castHit = RayCasterView.PoolRay(GroundDetectionModel.GroundDetectionInfo);
            var maxSlope = GroundDetectionModel.MaxSlope;
            for (int i = 0; i < castHit.Length; i++)
            {
                var normal = castHit[i].normal;
                var slope = Calculator.NormalToSlope(normal);
                var isGround = maxSlope > slope;
                
                if (isGround)
                {
                    return;
                }
            }

            StateEntity.ChangeState(PlayerStateType.Frying);
        }

        private void ToCharge(TouchStartEventArgument touchInfo)
        {
            StateEntity.ChangeState(PlayerStateType.Aiming);
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IRayCasterView RayCasterView { get; }
        private IGroundDetectionModel GroundDetectionModel { get; }
        private ITouchView TouchView { get; }
    }
}
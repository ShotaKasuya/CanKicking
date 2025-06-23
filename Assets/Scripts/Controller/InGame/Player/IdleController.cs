//
// namespace Domain.UseCase.InGame.Player
// {
//     public class IdleController : PlayerStateBehaviourBase
//     {
//         // public IdleController
//         // (
//         //     IPlayerGroundPresenter playerGroundPresenter,
//         //     IIsGroundedEntity isGroundedEntity,
//         //     IGroundingInfoRepository groundingInfoRepository,
//         //     ITouchPresenter touchPresenter,
//         //     IMutStateEntity<PlayerStateType> stateEntity
//         // ) : base(PlayerStateType.Idle, stateEntity)
//         // {
//         //     GroundPresenter = playerGroundPresenter;
//         //     IsGroundedEntity = isGroundedEntity;
//         //     GroundingInfoRepository = groundingInfoRepository;
//         //     TouchPresenter = touchPresenter;
//         // }
//         //
//         // public override void StateUpdate(float deltaTime)
//         // {
//         //     var castHit = GroundPresenter.PoolGround();
//         //     for (int i = 0; i < castHit.Length; i++)
//         //     {
//         //         var normal = castHit[i].normal;
//         //
//         //         var isGround = IsGroundedEntity.IsGround(new CheckGroundParams(
//         //             normal,
//         //             GroundingInfoRepository.MaxSlope
//         //         ));
//         //         if (isGround)
//         //         {
//         //             return;
//         //         }
//         //     }
//         //
//         //     StateEntity.ChangeState(PlayerStateType.Frying);
//         // }
//         //
//         // public override void OnEnter()
//         // {
//         //     TouchPresenter.OnTouch += ToCharge;
//         // }
//         //
//         // public override void OnExit()
//         // {
//         //     TouchPresenter.OnTouch -= ToCharge;
//         // }
//         //
//         // private void ToCharge(FingerTouchInfo touchInfo)
//         // {
//         //     StateEntity.ChangeState(PlayerStateType.Aiming);
//         // }
//         //
//         // private IIsGroundedEntity IsGroundedEntity { get; }
//         // private IPlayerGroundPresenter GroundPresenter { get; }
//         // private IGroundingInfoRepository GroundingInfoRepository { get; }
//         // private ITouchPresenter TouchPresenter { get; }
//     }
// }
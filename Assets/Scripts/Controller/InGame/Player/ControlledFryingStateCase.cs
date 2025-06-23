// namespace Domain.UseCase.InGame.Player
// {
//     /// <summary>
//     /// 回転を物理によらず、独自実装でコントロールする飛行状態
//     /// </summary>
//     // public class ControlledFryingStateCase : PlayerStateBehaviourBase, IDisposable
//     // {
//     //     public ControlledFryingStateCase
//     //     (
//     //         IPlayerPresenter playerPresenter,
//     //         IRotationStopPresenter rotationStopPresenter,
//     //         IPlayerContactPresenter playerContactPresenter,
//     //         IRotationStateRepository rotationStateRepository,
//     //         IGroundingInfoRepository groundingInfoRepository,
//     //         ITimeScaleRepository timeScaleRepository,
//     //         IIsGroundedEntity isGroundedEntity,
//     //         IIsStopedEntity isStopedEntity,
//     //         IMutStateEntity<PlayerStateType> stateEntity
//     //     ) : base(PlayerStateType.Frying, stateEntity)
//     //     {
//     //         PlayerPresenter = playerPresenter;
//     //         RotationStopPresenter = rotationStopPresenter;
//     //         PlayerContactPresenter = playerContactPresenter;
//     //         RotationStateRepository = rotationStateRepository;
//     //         GroundingInfoRepository = groundingInfoRepository;
//     //         TimeScaleRepository = timeScaleRepository;
//     //         IsGroundedEntity = isGroundedEntity;
//     //         IsStopedEntity = isStopedEntity;
//     //
//     //         PlayerContactPresenter.OnCollision += OnContact;
//     //     }
//     //
//     //     public override void OnEnter()
//     //     {
//     //         Time.timeScale = TimeScaleRepository.FryState;
//     //         RotationStopPresenter.Stop();
//     //     }
//     //
//     //     public override void OnExit()
//     //     {
//     //         Time.timeScale = ITimeScaleRepository.Normal;
//     //         RotationStopPresenter.ReStart();
//     //     }
//     //
//     //     private void OnContact(Collision2D collision)
//     //     {
//     //         if (!IsInState()) return;
//     //         
//     //         var isGround = CheckGrounded(collision.contacts);
//     //         if (!isGround)
//     //         {
//     //             RotationStateRepository.Toggle();
//     //         }
//     //     }
//     //     
//     //     private bool CheckGrounded(ContactPoint2D[] contactPoints)
//     //     {
//     //         for (int i = 0; i < contactPoints.Length; i++)
//     //         {
//     //             var normal = contactPoints[i].normal;
//     //
//     //             var isGround = IsGroundedEntity.IsGround(new CheckGroundParams(
//     //                 normal,
//     //                 GroundingInfoRepository.MaxSlope
//     //             ));
//     //             if (isGround)
//     //             {
//     //                 StateEntity.ChangeState(PlayerStateType.Idle);
//     //                 return true;
//     //             }
//     //         }
//     //
//     //         return false;
//     //     }
//     //
//     //     public override void StateUpdate(float deltaTime)
//     //     {
//     //         PlayerPresenter.Rotate(RotationStateRepository.RotationAngle * deltaTime);
//     //         if (IsStopedEntity.IsStop(PlayerPresenter.LinearVelocity(), PlayerPresenter.AnglerVelocity()))
//     //         {
//     //             StateEntity.ChangeState(PlayerStateType.Idle);
//     //         }
//     //     }
//     //
//     //     private IPlayerPresenter PlayerPresenter { get; }
//     //     private IRotationStopPresenter RotationStopPresenter { get; }
//     //     private IPlayerContactPresenter PlayerContactPresenter { get; }
//     //     private IGroundingInfoRepository GroundingInfoRepository { get; }
//     //     private IRotationStateRepository RotationStateRepository { get; }
//     //     private ITimeScaleRepository TimeScaleRepository { get; }
//     //     private IIsGroundedEntity IsGroundedEntity { get; }
//     //     private IIsStopedEntity IsStopedEntity { get; }
//     //
//     //     public void Dispose()
//     //     {
//     //         PlayerContactPresenter.OnCollision -= OnContact;
//     //     }
//     // }
// }
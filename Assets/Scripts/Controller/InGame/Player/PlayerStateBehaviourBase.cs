// using System.Collections.Generic;
// using Module.StateMachine;
// using Structure.InGame.Player;
// using Structure.Util;
//
// namespace Domain.UseCase.InGame.Player
// {
//     /// <summary>
//     /// ステートフルなロジックへの型エイリアス
//     /// </summary>
//     public class PlayerStateMachine : StateMachineBase<PlayerStateType>
//     {
//         public PlayerStateMachine
//         (
//             IState<PlayerStateType> state,
//             IReadOnlyList<IStateBehaviour<PlayerStateType>> behaviourEntities
//         ) : base(state, behaviourEntities)
//         {
//         }
//     }
//
//     public abstract class PlayerStateBehaviourBase : StateBehaviour<PlayerStateType>
//     {
//         protected PlayerStateBehaviourBase
//         (
//             PlayerStateType playerStateType,
//             IMutStateEntity<PlayerStateType> stateEntity
//         ) : base(playerStateType, stateEntity)
//         {
//         }
//     }
// }
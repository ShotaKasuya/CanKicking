using System.Collections.Generic;
using Module.StateMachine;
using Structure.InGame.Player;
using UnityEngine;
using VContainer.Unity;

namespace Domain.IUseCase.InGame
{
    /// <summary>
    /// ステートフルなロジックへの型エイリアス
    /// </summary>
    public class PlayerStateMachine : AbstractStateMachine<PlayerStateType>, ITickable
    {
        public PlayerStateMachine
        (
            IStateEntity<PlayerStateType> stateEntity,
            IReadOnlyList<IStateBehaviour<PlayerStateType>> behaviourEntities
        ) : base(stateEntity, behaviourEntities)
        {
        }

        public void Tick()
        {
            OnTick(Time.deltaTime);
        }
    }

    public abstract class PlayerStateBehaviourBase : StateBehaviour<PlayerStateType>
    {
        protected PlayerStateBehaviourBase
        (
            PlayerStateType playerStateType,
            IMutStateEntity<PlayerStateType> stateEntity
        ) : base(playerStateType, stateEntity)
        {
        }
    }

    public class PlayerStateEntity : AbstractStateType<PlayerStateType>
    {
        public PlayerStateEntity
        (
            PlayerStateType entryState
        ) : base(entryState)
        {
        }
    }
}
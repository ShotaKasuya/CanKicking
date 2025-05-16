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
            IState<PlayerStateType> state,
            IReadOnlyList<IStateBehaviour<PlayerStateType>> behaviourEntities
        ) : base(state, behaviourEntities)
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

    public class PlayerState : AbstractStateType<PlayerStateType>
    {
        public PlayerState() : base(PlayerStateType.Idle)
        {
        }
    }
}
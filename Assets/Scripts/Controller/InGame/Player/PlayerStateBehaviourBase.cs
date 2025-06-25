using System;
using System.Collections.Generic;
using Module.StateMachine;
using Structure.InGame.Player;
using Structure.Utility;
using UnityEngine;

namespace Controller.InGame.Player
{
    /// <summary>
    /// ステートフルなロジックへの型エイリアス
    /// </summary>
    public class PlayerStateMachine : StateMachineBase<PlayerStateType>
    {
        public PlayerStateMachine
        (
            IState<PlayerStateType> state,
            IReadOnlyList<IStateBehaviour<PlayerStateType>> behaviourEntities
        ) : base(state, behaviourEntities)
        {
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
    
    public class PlayerState: IMutStateEntity<PlayerStateType>
    {
        public PlayerStateType State { get; private set; }
        public bool IsInState(PlayerStateType state)
        {
            return State == state;
        }

        public Action<StatePair<PlayerStateType>> OnChangeState { get; set; }
        public void ChangeState(PlayerStateType next)
        {
            Debug.Log(next);
            State = next;
        }
    }
}
using System;
using DataUtil.InGame.Player;
using Module.StateMachine;

namespace Domain.IUseCase.InGame
{
    /// <summary>
    /// ステートフルなプレイヤーのユースケースへの型エイリアス
    /// </summary>
    public abstract class PlayerStateBehaviourBase : StateBehaviour<PlayerStateType>
    {
        protected PlayerStateBehaviourBase(IMutStateEntity<PlayerStateType> stateEntity) : base(stateEntity)
        {
        }
    }
    public class PlayerStateEntity: IMutStateEntity<PlayerStateType>
    {
        public PlayerStateType State { get; private set; }
        public bool IsInState(PlayerStateType state)
        {
            return State.HasFlag(state);
        }

        public Action<StatePair<PlayerStateType>> OnChangeState { get; set; }
        public void ChangeState(PlayerStateType next)
        {
            State = next;
        }
    }
}
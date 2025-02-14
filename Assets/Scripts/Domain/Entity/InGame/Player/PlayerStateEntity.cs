using System;
using DataUtil.InGame.Player;
using Domain.IEntity.InGame.Player;
using Module.StateMachine;

namespace Domain.Entity.InGame.Player
{
    public class PlayerStateEntity: IPlayerStateEntity
    {
        public PlayerStateType State { get; private set; }

        public bool IsInState(PlayerStateType state)
        {
            return State == state;
        }

        public Action<StatePair<PlayerStateType>> OnChangeState { get; set; }

        public void ChangeState(PlayerStateType state)
        {
            OnChangeState?.Invoke(new StatePair<PlayerStateType>(State, state));
            State = state;
        }
    }
}
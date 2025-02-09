using System;

namespace Module.StateMachine
{
    public interface IStateEntity<TState> where TState: struct, Enum
    {
        public TState State { get; }
        public bool IsInState(TState state);
        public void ChangeState(TState state);
    }
}
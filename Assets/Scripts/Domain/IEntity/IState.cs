using System;

namespace Domain.IEntity
{
    public interface IState<TState> where TState : Enum
    {
        public TState State { get; }
    }
}
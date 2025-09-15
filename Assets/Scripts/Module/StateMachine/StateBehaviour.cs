using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Module.StateMachine
{
    public abstract class AbstractAsyncStateBehaviour<TState> : IAsyncStateBehaviour<TState> where TState : struct, Enum
    {
        protected AbstractAsyncStateBehaviour(TState stateMask, IMutAsyncStateType<TState> innerState)
        {
            TargetStateMask = stateMask;
            InnerState = innerState;
        }

        public TState TargetStateMask { get; }

        protected IMutAsyncStateType<TState> InnerState { get; }

        protected bool IsInState()
        {
            return EqualityComparer<TState>.Default.Equals(TargetStateMask, InnerState.CurrentState);
        }

        public virtual UniTask OnEnter(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnExit(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }

        public virtual void StateUpdate(float deltaTime)
        {
        }
    }

    public abstract class AbstractStateBehaviour<TState> : IStateBehaviour<TState> where TState : struct, Enum
    {
        protected AbstractStateBehaviour(TState state, IMutStateType<TState> innerState)
        {
            TargetStateMask = state;
            InnerState = innerState;
        }

        public TState TargetStateMask { get; }
        protected IMutStateType<TState> InnerState { get; }


        protected bool IsInState()
        {
            return EqualityComparer<TState>.Default.Equals(TargetStateMask, InnerState.CurrentState);
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual void StateUpdate(float deltaTime)
        {
        }
    }
}
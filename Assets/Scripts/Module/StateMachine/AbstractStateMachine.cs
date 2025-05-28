using System;
using System.Collections.Generic;

namespace Module.StateMachine
{
    public abstract class AbstractStateMachine<TState> : IDisposable where TState : struct, Enum
    {
        protected AbstractStateMachine(IState<TState> state,
            IReadOnlyList<IStateBehaviour<TState>> behaviourEntities)
        {
            State = state;
            StateBehaviourEntities = behaviourEntities;
        }

        protected void Init()
        {
            State.OnChangeState += OnChangeState;
            CallOnEnter(State.State);
        }

        protected void OnTick(float deltaTime)
        {
            var currentState = State.State;
            for (int i = 0; i < StateBehaviourEntities.Count; i++)
            {
                var behaviour = StateBehaviourEntities[i];

                if (EqualityComparer<TState>.Default.Equals(currentState, behaviour.TargetStateMask))
                {
                    behaviour.StateUpdate(deltaTime);
                }
            }
        }

        private void OnChangeState(StatePair<TState> statePair)
        {
            CallOnExit(statePair.PrevState);
            CallOnEnter(statePair.NextState);
        }

        private IState<TState> State { get; }
        private IReadOnlyList<IStateBehaviour<TState>> StateBehaviourEntities { get; }

        private void CallOnEnter(TState prev)
        {
            for (int i = 0; i < StateBehaviourEntities.Count; i++)
            {
                var behaviour = StateBehaviourEntities[i];
                if (EqualityComparer<TState>.Default.Equals(prev, behaviour.TargetStateMask))
                {
                    behaviour.OnEnter();
                }
            }
        }

        private void CallOnExit(TState next)
        {
            for (int i = 0; i < StateBehaviourEntities.Count; i++)
            {
                var behaviour = StateBehaviourEntities[i];
                if (EqualityComparer<TState>.Default.Equals(next, behaviour.TargetStateMask))
                {
                    behaviour.OnExit();
                }
            }
        }

        public void Dispose()
        {
            State.OnChangeState -= OnChangeState;
        }
    }
}
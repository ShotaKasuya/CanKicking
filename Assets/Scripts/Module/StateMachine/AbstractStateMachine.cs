using System;
using System.Collections.Generic;

namespace Module.StateMachine
{
    public abstract class AbstractStateMachine<TState> : IDisposable where TState : struct, Enum
    {
        protected AbstractStateMachine(IStateEntity<TState> stateEntity,
            IReadOnlyList<IStateBehaviour<TState>> behaviourEntities)
        {
            StateEntity = stateEntity;
            StateBehaviourEntities = behaviourEntities;

            StateEntity.OnChangeState += OnChangeState;
            CallOnEnter(default);
        }

        protected void OnTick(float deltaTime)
        {
            var currentState = StateEntity.State;
            for (int i = 0; i < StateBehaviourEntities.Count; i++)
            {
                var behaviour = StateBehaviourEntities[i];

                if (currentState.Equals(behaviour.TargetStateMask))
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

        private IStateEntity<TState> StateEntity { get; }
        private IReadOnlyList<IStateBehaviour<TState>> StateBehaviourEntities { get; }

        private void CallOnEnter(TState prev)
        {
            for (int i = 0; i < StateBehaviourEntities.Count; i++)
            {
                var behaviour = StateBehaviourEntities[i];
                if (prev.Equals(behaviour.TargetStateMask))
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
                if (next.Equals(behaviour.TargetStateMask))
                {
                    behaviour.OnExit();
                }
            }
        }

        public void Dispose()
        {
            StateEntity.OnChangeState -= OnChangeState;
        }
    }
}
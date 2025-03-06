using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Module.Installer;

namespace Module.StateMachine
{
    public abstract class AbstractStateMachine<TState>: ITickable, IDisposable where TState: struct, Enum
    {
        protected AbstractStateMachine(IStateEntity<TState> stateEntity, List<IStateBehaviour<TState>> behaviourEntities)
        {
            StateEntity = stateEntity;
            StateBehaviourEntities = behaviourEntities;

            StateEntity.OnChangeState += OnChangeState;
            CallOnEnter(default);
        }
        
        public void Tick(float deltaTime)
        {
            var currentState = StateEntity.State;
            for (int i = 0; i < StateBehaviourEntities.Count; i++)
            {
                var behaviour = StateBehaviourEntities[i];

                if (IsEqual(behaviour.TargetStateMask, currentState))
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
        private List<IStateBehaviour<TState>> StateBehaviourEntities { get; }

        private void CallOnEnter(TState prev)
        {
            for (int i = 0; i < StateBehaviourEntities.Count; i++)
            {
                var behaviour = StateBehaviourEntities[i];
                if (IsEqual(behaviour.TargetStateMask, prev))
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
                if (IsEqual(behaviour.TargetStateMask, next))
                {
                    behaviour.OnExit();
                }
            }
        }

        public void Dispose()
        {
            StateEntity.OnChangeState -= OnChangeState;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsEqual(TState lhs, TState rhs)
        {
            return Convert.ToInt32(lhs) == Convert.ToInt32(rhs);
        }
    }
}
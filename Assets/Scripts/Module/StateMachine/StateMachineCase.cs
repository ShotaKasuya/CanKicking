using System;
using System.Collections.Generic;
using Module.Installer;

namespace Module.StateMachine
{
    public class StateMachineCase<TState>: ITickable, IDisposable where TState: struct, Enum
    {
        public StateMachineCase(IStateEntity<TState> stateEntity, List<IStateBehaviourEntity<TState>> behaviourEntities)
        {
            StateEntity = stateEntity;
            StateBehaviourEntities = behaviourEntities;

            StateEntity.OnChangeState += OnChangeState;
        }
        
        public void Tick(float deltaTime)
        {
            var currentState = StateEntity.State;
            for (int i = 0; i < StateBehaviourEntities.Count; i++)
            {
                var behaviour = StateBehaviourEntities[i];

                if (IsEqual(behaviour.TargetStateMask, currentState))
                {
                    behaviour.Tick(deltaTime);
                }
            }
        }

        private void OnChangeState(StatePair<TState> statePair)
        {
            for (int i = 0; i < StateBehaviourEntities.Count; i++)
            {
                var behaviour = StateBehaviourEntities[i];
                if (IsEqual(behaviour.TargetStateMask, statePair.PrevState))
                {
                    behaviour.OnExit(statePair.NextState);
                }
                if (IsEqual(behaviour.TargetStateMask, statePair.NextState))
                {
                    behaviour.OnEnter(statePair.PrevState);
                }
            }
        }

        private static bool IsEqual(TState lhs, TState rhs)
        {
            return Convert.ToInt32(lhs) == Convert.ToInt32(rhs);
        }
        
        private IStateEntity<TState> StateEntity { get; }
        private List<IStateBehaviourEntity<TState>> StateBehaviourEntities { get; }

        public void Dispose()
        {
            StateEntity.OnChangeState -= OnChangeState;
            foreach (var stateBehaviourEntity in StateBehaviourEntities)
            {
                stateBehaviourEntity.Dispose();
            }
        }
    }
}
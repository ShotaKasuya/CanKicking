using System;
using System.Collections.Generic;
using Module.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace Structure.Util
{
    public abstract class StateMachineBase<TState> : AbstractStateMachine<TState>, IStartable, ITickable
        where TState : struct, Enum
    {
        protected StateMachineBase
        (
            IState<TState> state,
            IReadOnlyList<IStateBehaviour<TState>> behaviourEntities
        ) : base(state, behaviourEntities)
        {
        }

        public void Start()
        {
            Init();
        }

        public void Tick()
        {
            OnTick(Time.deltaTime);
        }
    }
}
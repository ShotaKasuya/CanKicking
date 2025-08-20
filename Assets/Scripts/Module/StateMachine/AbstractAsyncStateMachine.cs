using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Module.StateMachine
{
    public abstract class AbstractAsyncStateMachine<TState> : IInitializable, ITickable where TState : struct, Enum
    {
        protected AbstractAsyncStateMachine
        (
            IState<TState> state,
            IReadOnlyList<IStateBehaviour<TState>> behaviours,
            CompositeDisposable compositeDisposable
        )
        {
            State = state;
            Behaviours = behaviours;
            Disposable = compositeDisposable;
        }


        public void Initialize()
        {
            State.StateExitObservable
                .Subscribe(this, (@enum, machine) => machine.CallOnExit(@enum))
                .AddTo(Disposable);
            State.StateEnterObservable
                .Subscribe(this, (@enum, machine) => machine.CallOnEnter(@enum))
                .AddTo(Disposable);
            CallOnEnter(State.CurrentState);
        }

        public void Tick()
        {
            var deltaTime = Time.deltaTime;
            var currentState = State.CurrentState;
            for (int i = 0; i < Behaviours.Count; i++)
            {
                var behaviour = Behaviours[i];

                if (EqualityComparer<TState>.Default.Equals(currentState, behaviour.TargetStateMask))
                {
                    behaviour.StateUpdate(deltaTime);
                }
            }
        }

        private void CallOnEnter(TState prev)
        {
            for (int i = 0; i < Behaviours.Count; i++)
            {
                var behaviour = Behaviours[i];
                if (EqualityComparer<TState>.Default.Equals(prev, behaviour.TargetStateMask))
                {
                    behaviour.OnEnter();
                }
            }
        }

        private void CallOnExit(TState next)
        {
            for (int i = 0; i < Behaviours.Count; i++)
            {
                var behaviour = Behaviours[i];
                if (EqualityComparer<TState>.Default.Equals(next, behaviour.TargetStateMask))
                {
                    behaviour.OnExit();
                }
            }
        }

        private CompositeDisposable Disposable { get; }
        private IState<TState> State { get; }
        private IReadOnlyList<IStateBehaviour<TState>> Behaviours { get; }
    }
}
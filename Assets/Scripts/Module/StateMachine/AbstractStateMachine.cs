using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Module.StateMachine
{
    public abstract class AbstractStateMachine<TState> : IStartable, ITickable where TState : struct, Enum
    {
        protected AbstractStateMachine
        (
            IStateType<TState> stateType,
            IReadOnlyList<IStateBehaviour<TState>> behaviours,
            CompositeDisposable compositeDisposable
        )
        {
            StateType = stateType;
            Behaviours = behaviours;
            Disposable = compositeDisposable;
        }

        public void Start()
        {
            StateType.StateExitObservable
                .Subscribe(
                    this,
                    (@enum, machine) => machine.CallOnExit(@enum))
                .AddTo(Disposable);
            StateType.StateEnterObservable
                .Subscribe(
                    this,
                    (@enum, machine) => machine.CallOnEnter(@enum))
                .AddTo(Disposable);

            var currentState = StateType.CurrentState;
            CallOnEnter(currentState);

            for (int i = 0; i < Behaviours.Count; i++)
            {
                var behaviour = Behaviours[i];
                if (!EqualityComparer<TState>.Default.Equals(currentState, behaviour.TargetStateMask))
                {
                    behaviour.OnExit();
                }
            }
        }

        public void Tick()
        {
            var deltaTime = Time.deltaTime;
            var currentState = StateType.CurrentState;
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
            const string stateExit = "State Exit";
            using var handle = StateType.GetStateLock(stateExit);
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
        private IStateType<TState> StateType { get; }
        private IReadOnlyList<IStateBehaviour<TState>> Behaviours { get; }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
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
            StateSequenceTasks = new List<UniTask>(behaviours.Count);
            Disposable = compositeDisposable;
        }

        public void Initialize()
        {
            State.StateExitObservable
                .SubscribeAwait(
                    this,
                    (@enum, machine, arg3) => machine.CallOnExit(@enum, arg3),
                    AwaitOperation.Parallel)
                .AddTo(Disposable);
            State.StateEnterObservable
                .SubscribeAwait(
                    this,
                    (@enum, machine, arg3) => machine.CallOnEnter(@enum, arg3),
                    AwaitOperation.Parallel)
                .AddTo(Disposable);
            
            var currentState = State.CurrentState;
            CallOnEnter(currentState).Forget();

            for (int i = 0; i < Behaviours.Count; i++)
            {
                var behaviour = Behaviours[i];
                if (!EqualityComparer<TState>.Default.Equals(currentState, behaviour.TargetStateMask))
                {
                    behaviour.OnExit(CancellationToken.None).Forget();
                }
            }
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

        private async UniTask CallOnEnter(TState prev, CancellationToken token = new CancellationToken())
        {
            for (int i = 0; i < Behaviours.Count; i++)
            {
                var behaviour = Behaviours[i];
                if (EqualityComparer<TState>.Default.Equals(prev, behaviour.TargetStateMask))
                {
                    StateSequenceTasks.Add(behaviour.OnEnter(token));
                }
            }

            await UniTask.WhenAll(StateSequenceTasks);
            StateSequenceTasks.Clear();
        }

        private async UniTask CallOnExit(TState next, CancellationToken token = new CancellationToken())
        {
            const string stateExit = "State Exit";
            using var handle = State.GetStateLock(stateExit);
            for (int i = 0; i < Behaviours.Count; i++)
            {
                var behaviour = Behaviours[i];
                if (EqualityComparer<TState>.Default.Equals(next, behaviour.TargetStateMask))
                {
                    StateSequenceTasks.Add(behaviour.OnExit(token));
                }
            }

            await UniTask.WhenAll(StateSequenceTasks);
            StateSequenceTasks.Clear();
        }

        private CompositeDisposable Disposable { get; }
        private IState<TState> State { get; }
        private IReadOnlyList<IStateBehaviour<TState>> Behaviours { get; }
        private List<UniTask> StateSequenceTasks { get; }
    }
}
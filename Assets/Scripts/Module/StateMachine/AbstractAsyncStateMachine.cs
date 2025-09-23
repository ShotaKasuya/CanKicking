using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Module.StateMachine
{
    public abstract class AbstractAsyncStateMachine<TState> : IAsyncStartable, ITickable where TState : struct, Enum
    {
        protected AbstractAsyncStateMachine
        (
            IStateType<TState> stateType,
            IReadOnlyList<IAsyncStateBehaviour<TState>> behaviours,
            CompositeDisposable compositeDisposable
        )
        {
            StateType = stateType;
            Behaviours = behaviours;
            StateSequenceTasks = new List<UniTask>(behaviours.Count);
            Disposable = compositeDisposable;
        }

        public async UniTask StartAsync(CancellationToken cancellation = new CancellationToken())
        {
            StateType.StateExitObservable
                .SubscribeAwait(
                    this,
                    (@enum, machine, arg3) => machine.CallOnExit(@enum, arg3),
                    AwaitOperation.Parallel)
                .AddTo(Disposable);
            StateType.StateEnterObservable
                .SubscribeAwait(
                    this,
                    (@enum, machine, arg3) => machine.CallOnEnter(@enum, arg3),
                    AwaitOperation.Parallel)
                .AddTo(Disposable);

            var currentState = StateType.CurrentState;
            await CallOnEnter(currentState, cancellation);

            for (int i = 0; i < Behaviours.Count; i++)
            {
                var behaviour = Behaviours[i];
                if (!EqualityComparer<TState>.Default.Equals(currentState, behaviour.TargetStateMask))
                {
                    await behaviour.OnExit(cancellation);
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

        private async UniTask CallOnEnter(TState prev, CancellationToken token = new CancellationToken())
        {
            const string stateEnter = "State Enter";
            using var handle = StateType.GetStateLock(stateEnter);
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
            using var handle = StateType.GetStateLock(stateExit);
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
        private IStateType<TState> StateType { get; }
        private IReadOnlyList<IAsyncStateBehaviour<TState>> Behaviours { get; }
        private List<UniTask> StateSequenceTasks { get; }
    }
}
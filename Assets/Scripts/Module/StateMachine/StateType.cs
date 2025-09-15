using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Module.Option.Runtime;
using R3;

namespace Module.StateMachine
{
    public abstract class AbstractAsyncStateType<TState> : IMutAsyncStateType<TState>
        where TState : struct, Enum
    {
        protected AbstractAsyncStateType
        (
            TState entryState
        )
        {
            CurrentState = entryState;
            EntryState = entryState;
            StateEnterSubject = new();
            StateExitSubject = new();
            StateLock = new();
        }

        public TState CurrentState { get; private set; }
        public TState EntryState { get; }
        public Observable<TState> StateExitObservable => StateExitSubject;
        public Observable<TState> StateEnterObservable => StateEnterSubject;

        private Subject<TState> StateEnterSubject { get; }
        private Subject<TState> StateExitSubject { get; }
        private OperationPool StateLock { get; }

        public bool IsInState(TState state)
        {
            return EqualityComparer<TState>.Default.Equals(CurrentState, state);
        }

        public async UniTask ChangeState(TState next)
        {
            await UniTask.WaitWhile(StateLock, pool => pool.IsAnyBlocked());
            StateExitSubject.OnNext(CurrentState);
            await UniTask.WaitWhile(StateLock, pool => pool.IsAnyBlocked());

            CurrentState = next;
            StateEnterSubject.OnNext(next);
        }

        public OperationHandle GetStateLock(string context)
        {
            return StateLock.SpawnOperation(context);
        }
    }

    public abstract class AbstractStateType<TState> : IMutStateType<TState>
        where TState : struct, Enum
    {
        protected AbstractStateType
        (
            TState entryState
        )
        {
            CurrentState = entryState;
            EntryState = entryState;
            StateEnterSubject = new();
            StateExitSubject = new();
            StateLock = new();
        }

        public TState CurrentState { get; private set; }
        public TState EntryState { get; }
        public Observable<TState> StateExitObservable => StateExitSubject;
        public Observable<TState> StateEnterObservable => StateEnterSubject;

        private Subject<TState> StateEnterSubject { get; }
        private Subject<TState> StateExitSubject { get; }
        private OperationPool StateLock { get; }

        public bool IsInState(TState state)
        {
            return EqualityComparer<TState>.Default.Equals(CurrentState, state);
        }

        public void ChangeState(TState next)
        {
            StateExitSubject.OnNext(CurrentState);

            CurrentState = next;
            StateEnterSubject.OnNext(next);
        }

        public OperationHandle GetStateLock(string context)
        {
            return StateLock.SpawnOperation(context);
        }
    }
}
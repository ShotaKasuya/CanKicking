using System;
using Cysharp.Threading.Tasks;
using Module.Option.Runtime;
using Module.StateMachine;
using R3;

namespace Tests.EditMode.Mocks
{
    public class MockStateEntity<T> : IMutStateEntity<T> where T : struct, Enum
    {
        public T CurrentState { get; private set; }
        public T EntryState { get; }
        public Observable<T> StateExitObservable => StateExitSubject;
        public Observable<T> StateEnterObservable => StateEnterSubject;

        private Subject<T> StateEnterSubject { get; } = new();
        private Subject<T> StateExitSubject { get; } = new();
        private OperationPool StateLock { get; } = new();

        public MockStateEntity(T initialState)
        {
            CurrentState = initialState;
            EntryState = initialState;
        }

        public bool IsInState(T state)
        {
            return System.Collections.Generic.EqualityComparer<T>.Default.Equals(CurrentState, state);
        }

        public async UniTask ChangeState(T next)
        {
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
}

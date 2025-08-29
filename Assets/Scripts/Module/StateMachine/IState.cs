using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Module.Option.Runtime;
using R3;

namespace Module.StateMachine
{
    public abstract class AbstractStateType<TState> : IMutStateEntity<TState>
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

    public interface IState<TState> where TState : struct, Enum
    {
        /// <summary>
        /// 現在のステート
        /// </summary>
        public TState CurrentState { get; }

        /// <summary>
        /// 初期ステート
        /// </summary>
        public TState EntryState { get; }

        /// <summary>
        /// そのステートであるなら`true`
        /// </summary>
        public bool IsInState(TState state);

        /// <summary>
        /// ステートのロックを取得する
        /// </summary>
        public OperationHandle GetStateLock(string context);

        /// <summary>
        /// ステートが変化する前のイベント
        /// </summary>
        public Observable<TState> StateExitObservable { get; }

        /// <summary>
        /// ステートが変化する後のイベント
        /// </summary>
        public Observable<TState> StateEnterObservable { get; }
    }

    public interface IMutStateEntity<TState> : IState<TState> where TState : struct, Enum
    {
        /// <summary>
        /// ステートを変化させる
        /// </summary>
        /// <param name="next">次のステート</param>
        public UniTask ChangeState(TState next);
    }
}
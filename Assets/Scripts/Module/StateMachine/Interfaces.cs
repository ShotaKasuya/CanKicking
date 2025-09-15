using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Module.Option.Runtime;
using R3;

namespace Module.StateMachine
{
    // ============================================================================================
    // State
    // ============================================================================================
    public interface IMutAsyncStateType<TState> : IStateType<TState> where TState : struct, Enum
    {
        /// <summary>
        /// ステートを変化させる
        /// </summary>
        /// <param name="next">次のステート</param>
        public UniTask ChangeState(TState next);
    }

    public interface IMutStateType<TState> : IStateType<TState> where TState : struct, Enum
    {
        /// <summary>
        /// ステートを変化させる
        /// </summary>
        /// <param name="next">次のステート</param>
        public void ChangeState(TState next);
    }

    public interface IStateType<TState> where TState : struct, Enum
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

    // ============================================================================================
    // Behaviour
    // ============================================================================================
    /// <summary>
    /// 非同期的に状態遷移を行うステートマシンにおける振る舞いを行うインターフェース
    /// </summary>
    /// <typeparam name="TState">ステートを示す型</typeparam>
    public interface IAsyncStateBehaviour<TState> where TState : struct, Enum
    {
        /// <summary>
        /// 実際に動作を行うステート
        /// </summary>
        public TState TargetStateMask { get; }

        /// <summary>
        /// 動作を行うステートに入った際に呼ばれる
        /// </summary>
        public UniTask OnEnter(CancellationToken token);

        /// <summary>
        /// ステートを出る際に呼ばれる
        /// </summary>
        public UniTask OnExit(CancellationToken token);

        /// <summary>
        /// `TargetState`の場合にまフレーム呼ばれる
        /// </summary>
        /// <param name="deltaTime">Time.deltaTime</param>
        public void StateUpdate(float deltaTime);
    }

    /// <summary>
    /// 同期的に状態遷移を行うステートマシンにおける振る舞いを行うインターフェース
    /// </summary>
    /// <typeparam name="TState">ステートを示す型</typeparam>
    public interface IStateBehaviour<TState> where TState : struct, Enum
    {
        /// <summary>
        /// 実際に動作を行うステート
        /// </summary>
        public TState TargetStateMask { get; }

        /// <summary>
        /// 動作を行うステートに入った際に呼ばれる
        /// </summary>
        public void OnEnter();

        /// <summary>
        /// ステートを出る際に呼ばれる
        /// </summary>
        public void OnExit();

        /// <summary>
        /// `TargetState`の場合にまフレーム呼ばれる
        /// </summary>
        /// <param name="deltaTime">Time.deltaTime</param>
        public void StateUpdate(float deltaTime);
    }
}
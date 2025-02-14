using System;

namespace Module.StateMachine
{
    /// <summary>
    /// ステートの振る舞いを行うインターフェース
    /// </summary>
    /// <typeparam name="TState">ステートを示す型</typeparam>
    public interface IStateBehaviourEntity<TState>: IDisposable where TState: struct, Enum
    {
        /// <summary>
        /// 実際に動作を行うステート
        /// </summary>
        public TState TargetStateMask { get; }

        /// <summary>
        /// 動作を行うステートに入った際に呼ばれる
        /// </summary>
        /// <param name="prev">前のステート</param>
        public void OnEnter(TState prev);

        /// <summary>
        /// ステートを出る際に呼ばれる
        /// </summary>
        /// <param name="next">次のステート</param>
        public void OnExit(TState next);

        /// <summary>
        /// 毎フレーム呼ばれる
        /// </summary>
        /// <param name="deltaTime">Time.deltaTime</param>
        public void Tick(float deltaTime);
    }
}
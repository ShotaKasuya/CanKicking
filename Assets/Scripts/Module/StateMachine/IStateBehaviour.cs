using System;
using System.Collections.Generic;

namespace Module.StateMachine
{
    /// <summary>
    /// ステートの振る舞いを行うインターフェース
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

    public abstract class StateBehaviour<TState> : IStateBehaviour<TState> where TState : struct, Enum
    {
        protected StateBehaviour(TState stateMask, IMutStateEntity<TState> stateEntity)
        {
            TargetStateMask = stateMask;
            StateEntity = stateEntity;
        }

        public TState TargetStateMask { get; }
        protected IMutStateEntity<TState> StateEntity { get; }

        protected bool IsInState()
        {
            return EqualityComparer<TState>.Default.Equals(TargetStateMask, StateEntity.CurrentState);
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual void StateUpdate(float deltaTime)
        {
        }
    }
}
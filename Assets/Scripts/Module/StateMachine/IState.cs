using System;
using System.Collections.Generic;

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
            State = entryState;
        }

        public TState State { get; private set; }
        public Action<StatePair<TState>> OnChangeState { get; set; }

        public bool IsInState(TState state)
        {
            return EqualityComparer<TState>.Default.Equals(State, state);
        }

        public virtual void ChangeState(TState next)
        {
            OnChangeState?.Invoke(new StatePair<TState>(State, next));
            State = next;
        }
    }

    public interface IState<TState> where TState : struct, Enum
    {
        /// <summary>
        /// 現在のステート
        /// </summary>
        public TState State { get; }

        /// <summary>
        /// そのステートであるなら`true`
        /// </summary>
        public bool IsInState(TState state);

        /// <summary>
        /// ステートが変化した際のイベント
        /// </summary>
        public Action<StatePair<TState>> OnChangeState { get; set; }
    }

    public interface IMutStateEntity<TState> : IState<TState> where TState : struct, Enum
    {
        /// <summary>
        /// ステートを変化させる
        /// </summary>
        /// <param name="next">次のステート</param>
        public void ChangeState(TState next);
    }

    public struct StatePair<TState> where TState : struct, Enum
    {
        public StatePair(TState prev, TState next)
        {
            PrevState = prev;
            NextState = next;
        }

        public TState PrevState { get; }
        public TState NextState { get; }
    }
}
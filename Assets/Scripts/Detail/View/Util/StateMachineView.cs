using System;
using UnityEngine;

namespace Detail.View.Util
{
    public abstract class StateMachineView<TState>: MonoBehaviour where TState: Enum
    {
        private TState _state;
    }
}
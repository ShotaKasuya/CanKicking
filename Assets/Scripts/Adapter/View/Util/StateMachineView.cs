using System;
using UnityEngine;

namespace Adapter.View.Util
{
    public abstract class StateMachineView<TState>: MonoBehaviour where TState: Enum
    {
        private TState _state;
    }
}
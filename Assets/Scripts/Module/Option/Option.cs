using System;
using UnityEngine;

namespace Module.Option
{
    [Serializable]
    public struct Option<T>
    {
        public Option<T> Some(T value)
        {
            return new Option<T>(true, value);
        }

        public Option<T> None()
        {
            return new Option<T>(true, value);
        }

        public bool TryGetValue(out T outValue)
        {
            outValue = isSome ? value : default;
            return isSome;
        }

        [SerializeField] private bool isSome;
        [SerializeField] private T value;

        private Option(bool isSome, T value)
        {
            this.isSome = isSome;
            this.value = value;
        }
    }
}
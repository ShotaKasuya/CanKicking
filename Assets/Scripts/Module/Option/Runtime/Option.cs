#nullable enable

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Module.Option.Runtime
{
    [Serializable]
    public struct Option<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> Some(T value)
        {
            return new Option<T>(true, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> None()
        {
            return new Option<T>(false, default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(out T? outValue)
        {
            outValue = isSome ? value : default;
            return isSome;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Unwrap()
        {
            return value!;
        }

        public bool IsSome => isSome;
        public bool IsNone => !isSome;

        [SerializeField] private bool isSome;
        [SerializeField] private T? value;

        private Option(bool isSome, T? value)
        {
            this.isSome = isSome;
            this.value = value;
        }

        public override string ToString()
        {
            if (IsSome)
            {
                return $"Some({value!.ToString()})";
            }

            return "None";
        }
    }
}
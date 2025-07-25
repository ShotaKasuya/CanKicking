using System;
using UnityEngine;

#nullable enable

namespace Module.Option
{
    [Serializable]
    public struct Option<T>
    {
        public static Option<T> Some(T value)
        {
            return new Option<T>(true, value);
        }

        public static Option<T> None()
        {
            return new Option<T>(false, default);
        }

        public bool TryGetValue(out T outValue)
        {
            outValue = isSome ? value : default;
            return isSome;
        }

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
using System;
using System.Runtime.CompilerServices;

#nullable enable

namespace Module.Option
{
    public struct OnceCell<T>
    {
        private Option<T> _innerValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Init(T value)
        {
            if (_innerValue.IsSome)
            {
                throw new InvalidOperationException("OnceCell initialize called many");
            }

            _innerValue = Option<T>.Some(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Get()
        {
            return _innerValue;
        }

        public bool IsInitialized => _innerValue.IsSome;
    }
}
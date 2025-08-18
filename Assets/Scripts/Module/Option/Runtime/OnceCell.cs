#nullable enable

using System;
using System.Runtime.CompilerServices;

namespace Module.Option.Runtime
{
    public class OnceCell<T>
    {
        public OnceCell()
        {
            _innerValue = Option<T>.None();
        }

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
        public bool TryUnwrap(out T? value)
        {
            var result = _innerValue.IsSome;
            value = result ? Unwrap() : default;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Unwrap()
        {
            return _innerValue.Unwrap();
        }

        public bool IsInitialized => _innerValue.IsSome;
    }
}
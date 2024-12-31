using System;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;

namespace DataUtil.Util
{
    public static class EnumExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InputPhaseType Conversion(this TouchPhase type)
        {
            return type switch
            {
                TouchPhase.Began => InputPhaseType.OnTouch,
                TouchPhase.Moved => InputPhaseType.Moving,
                TouchPhase.Stationary => InputPhaseType.Staying,
                TouchPhase.Ended => InputPhaseType.OnRelease,
                TouchPhase.Canceled => InputPhaseType.SystemCanceled,
                _ => throw new NotImplementedException()
            };
        }
    }
}
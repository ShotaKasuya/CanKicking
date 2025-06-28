using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

namespace Structure.Utility.Abstraction
{
    public class TouchDeltaMagnitudeComparer : IComparer<TouchState>
    {
        public int Compare(TouchState x, TouchState y)
        {
            var lenX = x.delta.sqrMagnitude;
            var lenY = y.delta.sqrMagnitude;

            if (lenX < lenY)
            {
                return -1;
            }

            if (lenX > lenY)
            {
                return 1;
            }

            return 0;
        }
    }
}
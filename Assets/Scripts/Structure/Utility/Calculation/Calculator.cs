using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Structure.Utility.Calculation
{
    public static class Calculator
    {
        /// <summary>
        /// 法線から傾斜を求める
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NormalToSlope(Vector2 normal)
        {
            return InnerAngleBetween(normal, Vector2.up);
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float InnerAngleBetween(float2 a, float2 b)
        {
            var dot = math.dot(math.normalize(a), math.normalize(b));
            dot = math.clamp(dot, -1f, 1f);
            var radianAngle = math.acos(dot);
            return math.degrees(radianAngle);
        }
    }
}
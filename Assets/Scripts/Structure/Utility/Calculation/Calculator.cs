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

        /// <summary>
        /// ベクトル間の角度を求める
        /// </summary>
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float InnerAngleBetween(float2 a, float2 b)
        {
            var dot = math.dot(math.normalize(a), math.normalize(b));
            var radianAngle = math.acos(dot);
            return math.degrees(radianAngle);
        }

        /// <summary>
        /// 画面の短辺にベクトルの大きさを合わせる
        /// </summary>
        /// <param name="origin">変換するベクトル</param>
        /// <param name="ratio"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 FitVectorToScreen(Vector2 origin, float ratio)
        {
            var width = Screen.width;
            var height = Screen.height;
            var shorter = math.min(width, height);
            var result = InnerFitVector(origin, ratio, shorter);

            return result;
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector2 InnerFitVector(float2 origin, float ratio, float length)
        {
            var result = origin / length;
            result *= ratio;
            result = math.length(result) > 1 ? math.normalize(result) : result;
            
            return result;
        }
    }
}
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
        /// 画面の短辺を長さ1として-1~1のベクトルに変換する
        /// </summary>
        /// <param name="origin">変換するベクトル</param>
        /// <param name="screen">画面の大きさ</param>
        /// <returns>正規化された元ベクトル, 画面に合わせたベクトルの長さ</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector2, float) FitVectorToScreen(Vector2 origin, Vector2 screen)
        {
            var shorter = math.min(screen.x, screen.y);
            var result = InnerFitVector(origin, shorter);

            return result;
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (float2, float) InnerFitVector(float2 origin, float length)
        {
            var originLength = math.length(origin);
            var fitLength = originLength / length;

            return (math.normalize(origin), fitLength);
        }
    }
}
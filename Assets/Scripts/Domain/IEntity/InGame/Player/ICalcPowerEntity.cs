using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Domain.IEntity.InGame.Player
{
    /// <summary>
    /// 引っ張って飛ばす際の力を計算する
    /// </summary>
    public interface ICalcPowerEntity
    {
        public Vector2 CalcPower(in CalcPowerParams calcPowerParams);
    }

    public readonly struct CalcPowerParams
    {
        public float2 DeltaPoint { get; }
        public float Power { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CalcPowerParams(Vector2 deltaPoint, float power)
        {
            DeltaPoint = deltaPoint;
            Power = power;
        }
    }
}
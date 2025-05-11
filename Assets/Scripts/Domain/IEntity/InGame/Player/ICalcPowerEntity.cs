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
        public float2 EndPoint { get; }
        public float2 StartPoint { get; }
        public float Power { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CalcPowerParams(Vector2 startPoint, Vector2 endPoint, float power)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Power = power;
        }
    }
}
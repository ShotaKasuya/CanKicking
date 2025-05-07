using UnityEngine;

namespace Domain.IEntity.InGame.Player
{
    /// <summary>
    /// 引っ張って飛ばす際の力を計算する
    /// </summary>
    public interface ICalcPowerEntity
    {
        public Vector2 CalcPower(in CalcPowerArg calcPowerArg);
    }

    public readonly struct CalcPowerArg
    {
        public Vector2 Difference { get; }
        public float Power { get; }

        public CalcPowerArg(Vector2 difference, float power)
        {
            Difference = difference;
            Power = power;
        }
    }
}
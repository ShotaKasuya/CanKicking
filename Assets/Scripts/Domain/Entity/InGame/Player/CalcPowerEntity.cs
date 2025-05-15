using Domain.IEntity.InGame.Player;
using Unity.Burst;
using UnityEngine;

namespace Domain.Entity.InGame.Player
{
    public class CalcPowerEntity : ICalcPowerEntity
    {
        [BurstCompile]
        public Vector2 CalcPower(in CalcPowerParams calcPowerParams)
        {
            var result = calcPowerParams.DeltaPoint * calcPowerParams.Power;
            return result;
        }
    }
}
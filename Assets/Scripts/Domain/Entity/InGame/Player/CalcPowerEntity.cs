using Domain.IEntity.InGame.Player;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Domain.Entity.InGame.Player
{
    [BurstCompile]
    public struct CalcPowerEntity : ICalcPowerEntity
    {
        public Vector2 CalcPower(in CalcPowerParams calcPowerParams)
        {
            var delta = calcPowerParams.EndPoint-calcPowerParams.StartPoint;
            var result = delta * -calcPowerParams.Power;
            return result;
        }
    }
}
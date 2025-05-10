using Domain.IEntity.InGame.Player;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Domain.Entity.InGame.Player
{
    [BurstCompile]
    public struct CalcPowerEntity : ICalcPowerEntity
    {
        public Vector2 CalcPower(in CalcPowerArg calcPowerArg)
        {
            var delta = calcPowerArg.EndPoint-calcPowerArg.StartPoint;
            var result = delta * -calcPowerArg.Power;
            return result;
        }
    }
}
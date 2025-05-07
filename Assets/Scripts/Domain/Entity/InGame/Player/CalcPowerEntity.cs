using Domain.IEntity.InGame.Player;
using Unity.Burst;
using UnityEngine;

namespace Domain.Entity.InGame.Player
{
    [BurstCompile]
    public struct CalcPowerEntity : ICalcPowerEntity
    {
        public Vector2 CalcPower(in CalcPowerArg calcPowerArg)
        {
            return calcPowerArg.Difference * -calcPowerArg.Power;
        }
    }
}
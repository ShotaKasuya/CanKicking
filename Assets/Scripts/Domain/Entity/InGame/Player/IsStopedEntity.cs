using Domain.IEntity.InGame.Player;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Domain.Entity.InGame.Player
{
    public class IsStopedEntity : IIsStopedEntity
    {
        private const float Threshold = 0.05f;

        [BurstCompile]
        public bool IsStop(Vector2 velocity, float anglerVelocity)
        {
            var velStop = math.lengthsq(velocity) < Threshold * Threshold;
            var angStop = anglerVelocity < Threshold;

            return velStop & angStop;
        }
    }
}
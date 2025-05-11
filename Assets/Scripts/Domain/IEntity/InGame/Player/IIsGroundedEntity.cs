using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Domain.IEntity.InGame.Player
{
    public interface IIsGroundedEntity
    {
        public bool IsGround(CheckGroundParams checkGroundParams);
    }

    public readonly struct CheckGroundParams
    {
        public float2 Normal { get; }
        public float SlopeLimit { get; }
        public float2 CurrentVelocity { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CheckGroundParams
        (
            Vector2 normal,
            float slopeLimit,
            Vector2 currentVelocity
        )
        {
            Normal = normal;
            SlopeLimit = slopeLimit;
            CurrentVelocity = currentVelocity;
        }
    }
}
using System.Runtime.CompilerServices;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CheckGroundParams
        (
            Vector2 normal,
            float slopeLimit
        )
        {
            Normal = normal;
            SlopeLimit = slopeLimit;
        }
    }
}
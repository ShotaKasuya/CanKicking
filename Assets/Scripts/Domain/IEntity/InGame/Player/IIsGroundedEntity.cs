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
        public NativeArray<RaycastHit2D> RaycastHits { get; }
        public float SlopeLimit { get; }
        public float2 CurrentVelocity { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CheckGroundParams
        (
            RaycastHit2D[] raycastHits,
            float slopeLimit,
            Vector2 currentVelocity
        )
        {
            RaycastHits = new NativeArray<RaycastHit2D>(raycastHits, Allocator.Temp);
            SlopeLimit = slopeLimit;
            CurrentVelocity = currentVelocity;
        }
    }
}
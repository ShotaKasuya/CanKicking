using System;
using Module.ImmutableClass;
using UnityEngine;

namespace Adapter.IView.InGame.Player
{
    public interface IPlayerView
    {
        public Pose PlayerPose { get; }
        public ConstRbody Rbody { get; }
    }

    public interface IMutPlayerView : IPlayerView
    {
        public Transform ModelTransform { get; }
        public Rigidbody2D MutRbody { get; }
    }

    public interface IPlayerCastView
    {
        public int CastFromPlayer(RayCastInfo rayCastInfo, RaycastHit2D[] result);
    }

    public interface IContactView
    {
        public Action<Collision2D> ContactEvent { get; set; } 
    }

    public readonly ref struct RayCastInfo
    {
        public Vector2 Direction { get; }
        public float Distance { get; }
        public int LayerMask { get; }

        public RayCastInfo(Vector2 direction, float distance, int mask)
        {
            Direction = direction;
            Distance = distance;
            LayerMask = mask;
        }
    }
}
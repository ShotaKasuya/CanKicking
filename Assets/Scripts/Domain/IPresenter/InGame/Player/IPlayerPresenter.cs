using System;
using UnityEngine;

namespace Domain.IPresenter.InGame.Player
{
    public interface IPlayerVelocityPresenter
    {
        public Vector2 LinearVelocity();
        public float AnglerVelocity();
    }

    public interface IPlayerGroundPresenter
    {
        public RaycastHit2D[] PoolGround();
    }

    public interface IPlayerContactPresenter
    {
        public Action<Collision2D> OnCollision { get; set; }
    }
}
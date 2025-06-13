using System;
using UnityEngine;

namespace Domain.IPresenter.InGame.Player
{
    public interface IPlayerPresenter
    {
        public Vector2 LinearVelocity();
        public float AnglerVelocity();
        public void Rotate(float value);
    }
    
    public interface IRotationStopPresenter
    {
        public void Stop();
        public void ReStart();
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
using UnityEngine;

namespace Domain.IPresenter.InGame.Player
{
    public interface IPlayerPresenter
    {
        public Vector3 Position { get; }
    }

    public interface IPlayerVelocityPresenter
    {
        public Vector2 LinearVelocity();
        public float AnglerVelocity();
    }

    public interface IPlayerGroundPresenter
    {
        public RaycastHit2D[] PoolGround();
    }
}
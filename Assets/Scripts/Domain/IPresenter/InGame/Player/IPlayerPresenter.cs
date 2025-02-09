using UnityEngine;

namespace Domain.IPresenter.InGame.Player
{
    public interface IPlayerPresenter
    {
        public Vector3 Position { get; }
    }

    public interface IPlayerSpeedPresenter
    {
        public float SqrSpeed();
        public float AnglerVelocity();
    }
}
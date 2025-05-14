using UnityEngine;

namespace Domain.IEntity.InGame.Player
{
    public interface IIsStopedEntity
    {
        public bool IsStop(Vector2 velocity, float anglerVelocity);
    }
}
using UnityEngine;

namespace Domain.IPresenter.InGame.Player
{
    public interface IKickPresenter
    {
        public void Kick(KickArg kickArg);
    }
    
    public struct KickArg
    {
        public KickArg(float power, Vector2 vector, float torque)
        {
            Power = power;
            Vector = vector;
            Torque = torque;
        }

        public float Power { get; }
        public Vector2 Vector { get; }
        public float Torque { get; }
    }
}
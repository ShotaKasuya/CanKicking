using UnityEngine;

namespace Domain.IPresenter.InGame.Player
{
    public interface IKickPresenter
    {
        public void Kick(KickArg kickArg);
    }
    
    public struct KickArg
    {
        public KickArg(Vector2 kickPower, float torque)
        {
            KickPower = kickPower;
            Torque = torque;
        }

        public Vector2 KickPower { get; }
        public float Torque { get; }
    }
}
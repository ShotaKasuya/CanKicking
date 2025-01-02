using UnityEngine;

namespace DataUtil.InGame.Player
{
    public struct KickArg
    {
        public KickArg(Vector2 vector, float torque)
        {
            Vector = vector;
            Torque = torque;
        }

        public Vector2 Vector { get; }
        public float Torque { get; }
    }
}
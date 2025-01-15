using UnityEngine;

namespace Domain.IPresenter.InGame.UI
{
    public interface IKickPowerPresenter
    {
        public void ToggleOn();
        public void ToggleOff();
        public void ShowPower(ShowKickPowerArg arg);
    }

    public struct ShowKickPowerArg
    {
        public ShowKickPowerArg
        (
            float power,
            Vector2 from,
            Vector2 to
        )
        {
            Power = power;
            From = from;
            To = to;
        }
        
        /// <summary>
        /// range: 0~1
        /// </summary>
        public float Power { get; }
        public Vector2 From { get; }
        public Vector2 To { get; }
    }
}
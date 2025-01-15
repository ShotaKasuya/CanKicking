using Adapter.IView.InGame.Player;
using Domain.IPresenter.InGame.Player;
using UnityEngine;

namespace Adapter.Presenter.InGame.Player
{
    public class PlayerKickPresenter : IKickPresenter
    {
        public PlayerKickPresenter
        (
            IMutPlayerView playerView
        )
        {
            PlayerView = playerView;
        }

        public void Kick(KickArg kickArg)
        {
            var rbody = PlayerView.MutRbody;
            var power = kickArg.Power;
            rbody.AddForce(kickArg.Vector * power, ForceMode2D.Impulse);
            rbody.AddTorque(kickArg.Torque * power, ForceMode2D.Impulse);
        }

        private IMutPlayerView PlayerView { get; }
    }
}
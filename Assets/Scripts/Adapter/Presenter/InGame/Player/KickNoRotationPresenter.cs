using Adapter.IView.InGame.Player;
using Domain.IPresenter.InGame.Player;
using UnityEngine;

namespace Adapter.Presenter.InGame.Player
{
    public class KickNoRotationPresenter: IKickPresenter
    {
        public KickNoRotationPresenter
        (
            IMutPlayerView playerView
        )
        {
            PlayerView = playerView;
        }

        public void Kick(KickArg kickArg)
        {
            var rbody = PlayerView.MutRbody;
            rbody.AddForce(kickArg.KickPower, ForceMode2D.Impulse);
        }

        private IMutPlayerView PlayerView { get; }
    }
}
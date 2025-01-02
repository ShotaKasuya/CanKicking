using Adapter.IView.InGame.Player;
using DataUtil.InGame.Player;
using Domain.IPresenter.InGame.Player;
using UnityEngine;

namespace Adapter.Presenter.InGame.Player
{
    public class PlayerKickPresenter: IKickPresenter
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
            
            rbody.AddForce(kickArg.Vector, ForceMode2D.Impulse);
            rbody.AddTorque(kickArg.Torque, ForceMode2D.Impulse);
        }
        
        private IMutPlayerView PlayerView { get; }
    }
}
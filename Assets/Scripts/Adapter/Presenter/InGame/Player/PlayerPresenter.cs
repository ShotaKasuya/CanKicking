using Adapter.IView.InGame.Player;
using Domain.IPresenter.InGame.Player;
using UnityEngine;

namespace Adapter.Presenter.InGame.Player
{
    public class PlayerPresenter: IPlayerPresenter, IPlayerSpeedPresenter
    {
        public PlayerPresenter
        (
            IPlayerView playerView
        )
        {
            PlayerView = playerView;
        }

        public Vector3 Position => PlayerView.PlayerPose.position;
        public float SqrSpeed()
        {
            return PlayerView.Rbody.Velocity.sqrMagnitude;
        }

        public float AnglerVelocity()
        {
            return PlayerView.Rbody.AnglerVelocity;
        }
        
        private IPlayerView PlayerView { get; }
    }
}
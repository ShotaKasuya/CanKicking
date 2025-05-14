using Adapter.IView.InGame.Player;
using Domain.IPresenter.InGame.Player;
using UnityEngine;

namespace Adapter.Presenter.InGame.Player
{
    public class PlayerPresenter : IPlayerVelocityPresenter
    {
        public PlayerPresenter
        (
            IPlayerView playerView
        )
        {
            PlayerView = playerView;
        }

        public Vector3 Position => PlayerView.PlayerPose.position;

        public Vector2 LinearVelocity()
        {
            return PlayerView.Rbody.Velocity;
        }

        public float AnglerVelocity()
        {
            return PlayerView.Rbody.AnglerVelocity;
        }

        private IPlayerView PlayerView { get; }
    }
}
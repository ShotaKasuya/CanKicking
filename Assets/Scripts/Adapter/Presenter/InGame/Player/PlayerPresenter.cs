using Adapter.IView.InGame.Player;
using Domain.IPresenter.InGame.Player;
using UnityEngine;

namespace Adapter.Presenter.InGame.Player
{
    public class PlayerPresenter : IPlayerPresenter
    {
        public PlayerPresenter
        (
            IMutPlayerView playerView
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

        public void Rotate(float value)
        {
            PlayerView.ModelTransform.Rotate(new Vector3(0, 0, value));
        }

        private IMutPlayerView PlayerView { get; }
    }
}
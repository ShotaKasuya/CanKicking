using Adapter.IView.InGame.Player;
using Domain.IPresenter.InGame.Player;
using UnityEngine;

namespace Adapter.Presenter.InGame.Player
{
    public class PlayerPresenter: IPlayerPresenter
    {
        public PlayerPresenter
        (
            IPlayerView playerView
        )
        {
            PlayerView = playerView;
        }

        public Vector3 Position => PlayerView.PlayerPose.position;
        
        private IPlayerView PlayerView { get; }
    }
}
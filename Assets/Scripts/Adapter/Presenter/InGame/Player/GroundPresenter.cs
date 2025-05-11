using Adapter.IView.InGame.Player;
using Domain.IPresenter.InGame.Player;
using UnityEngine;

namespace Adapter.Presenter.InGame.Player
{
    public class GroundPresenter : IPlayerGroundPresenter
    {
        public GroundPresenter
        (
            IPlayerCastView playerCastView
        )
        {
            PlayerCastView = playerCastView;
        }

        public RaycastHit2D[] PoolGround()
        {
            PlayerCastView.CastFromPlayer(new RayCastInfo(
                Vector2.down,
                1, 1), RayCastCache);
            return RayCastCache;
        }

        private RaycastHit2D[] RayCastCache { get; } = new RaycastHit2D[100];
        private IPlayerCastView PlayerCastView { get; }
    }
}
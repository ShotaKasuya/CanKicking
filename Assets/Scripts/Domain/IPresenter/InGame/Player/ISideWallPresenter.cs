using UnityEngine;

namespace Domain.IPresenter.InGame.Player
{
    public interface ISideWallPresenter
    {
        public WallPoint RightSide { get; }
        public WallPoint LeftSide { get; }
    }

    public struct WallPoint
    {
        public WallPoint(RaycastHit2D raycastHit2D)
        {
            Point = raycastHit2D.point;
            Distance = raycastHit2D.distance;
        }
        
        public Vector2 Point { get; }
        public float Distance { get; }
    }
}
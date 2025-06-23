using System.Runtime.CompilerServices;
using R3;
using Structure.Util;
using UnityEngine;

namespace Interface.InGame.Player
{
    /// <summary>
    /// プレイヤーの情報
    /// </summary>
    public interface IPlayerView
    {
        public Vector2 LinearVelocity { get; }
        public Vector2 AngularVelocity { get; }
        
        public Observable<Collision2D> CollisionEnterEvent { get; }
    }
    
    /// <summary>
    /// プレイヤーからレイキャストを行うためのインターフェース
    /// </summary>
    public interface IRayCasterView
    {
        public RaycastHit[] PoolRay(RayCastInfo rayCastInfo);
    }

    public interface ITouchView
    {
        public Observable<TouchStartEventArgument> TouchEvent { get; }
        public FingerDraggingInfo DraggingInfo { get; }
        public Observable<TouchEndEventArgument> TouchEndEvent { get; }
    }

    public readonly struct TouchStartEventArgument
    {
        public Vector2 TouchPosition { get; }

        public TouchStartEventArgument(Vector2 touchPosition)
        {
            TouchPosition = touchPosition;
        }
    }

    public readonly ref struct FingerDraggingInfo
    {
        public Vector2 TouchStartPosition { get; }
        public Vector2 CurrentPosition { get; }

        public FingerDraggingInfo(Vector2 touchStartPosition, Vector2 currentPosition)
        {
            TouchStartPosition = touchStartPosition;
            CurrentPosition = currentPosition;
        }
    }

    public readonly struct TouchEndEventArgument
    {
        public Vector2 TouchStartPosition { get; }
        public Vector2 TouchEndPosition { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TouchEndEventArgument(Vector2 touchStartPosition, Vector2 touchEndPosition)
        {
            TouchStartPosition = touchStartPosition;
            TouchEndPosition = touchEndPosition;
        }
    }
}
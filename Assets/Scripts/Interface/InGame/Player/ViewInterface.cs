using System;
using System.Runtime.CompilerServices;
using Module.Option;
using R3;
using Structure.Utility;
using UnityEngine;

namespace Interface.InGame.Player;

/// <summary>
/// プレイヤーの情報を提供するインターフェース
/// </summary>
public interface IPlayerView
{
    public Transform ModelTransform { get; }
    public Vector2 LinearVelocity { get; }
    public float AngularVelocity { get; }

    public Observable<Collision2D> CollisionEnterEvent { get; }
}

/// <summary>
/// プレイヤーが狙っている方向を提示するインターフェース
/// </summary>
public interface IAimView
{
    public void SetAim(Vector2 aimVector);
    public void Show();
    public void Hide();
}

/// <summary>
/// プレイヤーを飛ばすためのインターフェース
/// </summary>
public interface ICanKickView
{
    public void Kick(KickContext context);
}

public readonly ref struct KickContext
{
    public Vector2 Direction { get; }
    public float RotationPower { get; }

    public KickContext(Vector2 direction, float rotationPower)
    {
        Direction = direction;
        RotationPower = rotationPower;
    }
}

// ============================================================================================
// 入力系
// ============================================================================================

/// <summary>
/// プレイヤーからレイキャストを行うためのインターフェース
/// </summary>
public interface IRayCasterView
{
    public ReadOnlySpan<RaycastHit2D> PoolRay(RayCastInfo rayCastInfo);
}

public interface ITouchView
{
    public Observable<TouchStartEventArgument> TouchEvent { get; }
    public Option<FingerDraggingInfo> DraggingInfo { get; }
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

public readonly struct FingerDraggingInfo
{
    public Vector2 TouchStartPosition { get; }
    public Vector2 CurrentPosition { get; }
    public Vector2 Delta => CurrentPosition - TouchStartPosition;

    public FingerDraggingInfo(Vector2 touchStartPosition, Vector2 currentPosition)
    {
        TouchStartPosition = touchStartPosition;
        CurrentPosition = currentPosition;
    }
}

public readonly struct TouchEndEventArgument
{
    private Vector2 TouchStartPosition { get; }
    private Vector2 TouchEndPosition { get; }
    public Vector2 Delta => TouchStartPosition - TouchEndPosition;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TouchEndEventArgument(Vector2 touchStartPosition, Vector2 touchEndPosition)
    {
        TouchStartPosition = touchStartPosition;
        TouchEndPosition = touchEndPosition;
    }
}
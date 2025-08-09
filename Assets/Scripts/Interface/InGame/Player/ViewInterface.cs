using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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

    public void Activation(bool isActive);
    public void ResetPosition(Vector2 position);
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

/// <summary>
/// エフェクトを再生するインターフェース
/// </summary>
public interface ISpawnEffectView
{
    public UniTask Initialize();
    public UniTask SpawnEffect(Vector2 spawnPoint, Vector2 angle, float duration, CancellationToken cancellationToken);
}

/// <summary>
/// プレイヤーからレイキャストを行うためのインターフェース
/// </summary>
public interface IRayCasterView
{
    public ReadOnlySpan<RaycastHit2D> PoolRay(RayCastInfo rayCastInfo);
}
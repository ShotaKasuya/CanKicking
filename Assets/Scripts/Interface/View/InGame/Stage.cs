using R3;
using UnityEngine;

namespace Interface.View.InGame;

public interface ISpawnPositionView
{
    public Transform StartPosition { get; }
}

public interface IBaseHeightView
{
    public float PositionY { get; }
}

public interface IGoalEventView
{
    public Observable<Unit> Performed { get; }
}

public interface IGoalHeightView
{
    public float PositionY { get; }
}

public interface ICameraView : ICameraInitializableView
{
    public void SetOrthoSize(float orthoSize);
}

public interface ICameraInitializableView
{
    public void Init(Transform playerTransform);
}

/// <summary>
/// ピンチイン・ピンチアウトの入力を受け取る
/// </summary>
public interface IPinchView
{
    public float Pool();
}

//====================================================================
// ステージギミック
//====================================================================

public interface IDisappearingPlatformView
{
    public Observable<Collision2D> OnCollision { get; }

    public void OnLandPlayer();
    public void Hide();
    public void Show();
}

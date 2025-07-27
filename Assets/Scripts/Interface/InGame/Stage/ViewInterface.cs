using Module.Option;
using R3;
using UnityEngine;

namespace Interface.InGame.Stage;

public interface IStartPositionView
{
    public Option<Transform> StartPosition { get; }
    public void SetStartPosition(Transform startPosition);
}

public interface IPlayerSpawnPositionView
{
    public Transform StartPosition { get; }
}

public interface IGoalEventView
{
    public Observable<Unit> Performed { get; }
}

public interface ICameraView
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
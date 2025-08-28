using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Interface.View.InGame.UserInterface;

/// <summary>
/// `Normal`状態UIの表示切り替えを行う
/// </summary>
public interface INormalUiView
{
    public UniTask Show(CancellationToken token);
    public UniTask Hide(CancellationToken token);
}

/// <summary>
/// 停止ボタンのイベントを提供する
/// </summary>
public interface IStopButtonView
{
    public Observable<Unit> Performed { get; }
}

/// <summary>
/// 高さを表示する
/// </summary>
public interface IHeightUiView
{
    public void SetHeight(float height);
}

/// <summary>
/// ジャンプ回数を表示する
/// </summary>
public interface IKickCountUiView
{
    public void SetCount(int count);
}

/// <summary>
/// ステージのクリア割合を保持する
/// </summary>
public interface IProgressUiView
{
    public void SetProgress(float progress);
}

public interface IPullRangeView
{
    public void ShowRange(AimContext aimContext);
    public void HideRange();
}

public readonly ref struct AimContext
{
    public AimContext
    (
        Vector2 startPoint,
        float cancelRadius,
        float maxRadius
    )
    {
        StartPoint = startPoint;
        CancelRadius = cancelRadius;
        MaxRadius = maxRadius;
    }

    public Vector2 StartPoint { get; }
    public float CancelRadius { get; }
    public float MaxRadius { get; }
}

/// <summary>
/// 引っ張りの範囲を表示する
/// </summary>
public interface IRangeView
{
    public void Set(Vector2 point, float radius);
    public void Hide();
}
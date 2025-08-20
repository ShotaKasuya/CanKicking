using Cysharp.Threading.Tasks;
using R3;

namespace Interface.InGame.UserInterface;

public interface IStopUiView
{
    public UniTask Show();
    public UniTask Hide();
}

/// <summary>
/// 停止状態から再開するボタンのView
/// </summary>
public interface IPlayButtonView
{
    public Observable<Unit> Performed { get; }
}

/// <summary>
/// ストップ時のリスタートボタン用View
/// </summary>
public interface IStop_RestartButtonView
{
    public Observable<string> Performed { get; }
}

/// <summary>
/// ストップ時のステージセレクトボタン用View
/// </summary>
public interface IStop_StageSelectButtonView
{
    public Observable<string> Performed { get; }
}
using R3;

namespace Interface.Model.InGame;

/// <summary>
/// ジャンプを行った回数をカウントするモデル
/// </summary>
public interface IKickCountModel
{
    public ReadOnlyReactiveProperty<int> KickCount { get; }
    public void Inc();
}

public interface IResetableModel
{
    public void Reset();
}
using R3;

namespace Interface.InGame.Primary;

/// <summary>
/// ジャンプを行った回数をカウントするモデル
/// </summary>
public interface IJumpCountModel
{
    public ReadOnlyReactiveProperty<int> JumpCount { get; }
    public void Inc();
}

public interface IResetableModel
{
    public void Reset();
}
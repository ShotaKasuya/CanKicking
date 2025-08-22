using Interface.InGame.Player;
using Interface.InGame.Stage;
using Module.Option;
using Module.Option.Runtime;

namespace Interface.InGame.Primary;

/// <summary>
/// 遅延初期化されるプレイヤーのView
/// </summary>
public interface ILazyPlayerView
{
    public OnceCell<IPlayerView> PlayerView { get; }
}

/// <summary>
/// 遅延初期化されるスタート地点のView
/// </summary>
public interface ILazyStartPositionView
{
    public OnceCell<ISpawnPositionView> StartPosition { get; }
}

/// <summary>
/// 遅延初期化されるステージのベースとなる高さのView
/// </summary>
public interface ILazyBaseHeightView
{
    public OnceCell<float> BaseHeight { get; }
}

/// <summary>
/// 遅延初期化されるゴールの高さのView
/// </summary>
public interface ILazyGoalHeightView
{
    public OnceCell<float> GoalHeight { get; }
}

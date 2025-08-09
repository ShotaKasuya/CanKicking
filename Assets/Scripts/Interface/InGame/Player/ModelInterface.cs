using Module.Option;
using Structure.Utility;
using UnityEngine;

namespace Interface.InGame.Player;

//====================================================================
// 設定データ
//====================================================================

/// <summary>
/// 地面の検出に関する情報を持つ
/// </summary>
public interface IGroundDetectionModel
{
    public RayCastInfo GroundDetectionInfo { get; }
    public float MaxSlope { get; }
}

/// <summary>
/// キックのベースとなる力を持つ
/// </summary>
public interface IKickBasePowerModel
{
    public float KickPower { get; }
    public float RotationPower { get; }
}

/// <summary>
/// 画面をどの程度引っ張ったところを最大・最小とするかの比率を持つ
/// </summary>
public interface IPullLimitModel
{
    public float CancelRatio { get; }
    public float MaxRatio { get; }
}

public interface IEffectSpawnModel
{
    public float SpawnThreshold { get; }
    public float EffectLength { get; }
}

//====================================================================
// 実行時データ
//====================================================================

/// <summary>
/// キックを行った場所のログを持つ
/// </summary>
public interface IKickPositionModel
{
    public Option<Vector2> PopPosition();
    public void PushPosition(Vector2 position);
}

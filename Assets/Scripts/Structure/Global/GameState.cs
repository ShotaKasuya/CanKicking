using System.Collections.Generic;
using MessagePack;
using Structure.InGame.Stage;
using UnityEngine;

namespace Structure.Global;

/// <summary>
/// ゲーム開始時、最初に読み込まれる。
/// このデータを参照して
/// * チュートリアルへの移動
/// * ステージセレクトでのクリア演出
/// が行われる
/// </summary>
public readonly struct UserState
{
    public GameState GameState { get; }

    /// <summary>
    /// `PlayerState`が`StageCleared`の場合にクリアしたステージが保存される
    /// </summary>
    public string ClearedStageName { get; }

    public Dictionary<string, StageProgressData> ProgressData { get; }

    public UserState(GameState state, string clearedStageName, Dictionary<string, StageProgressData> progressData)
    {
        GameState = state;
        ClearedStageName = clearedStageName;
        ProgressData = progressData;
    }
}

public enum GameState
{
    /// <summary>
    /// チュートリアル未クリアの状態
    /// ゲーム起動後、チュートリアルに移行する
    /// </summary>
    Tutorial,

    /// <summary>
    /// 通常状態
    /// ゲーム起動後、タイトルを経てステージ選択画面に移行する
    /// </summary>
    Normal,

    /// <summary>
    /// ステージクリア状態
    /// ゲーム起動後、タイトルを経てステージ選択画面に移行後、
    /// ステージクリア演出を行う
    /// </summary>
    StageCleared,
}

/// <summary>
/// ステージセレクトシーンで見るクリア状況のデータ
/// </summary>
[MessagePackObject]
public class StageProgressData
{
    [Key(0)] public StageState StageState;
    [Key(1)] public int BestKickCount;

    public StageProgressData(StageState state, int bestKickCount)
    {
        StageState = state;
        BestKickCount = bestKickCount;
    }
}

/// <summary>
/// ステージ中断時に生成されるセーブデータ
/// </summary>
public readonly struct StageData
{
    public Vector3 PlayerPosition { get; }
    public Quaternion PlayerRotation { get; }
    public (Vector3, Quaternion)[] KickPositions { get; }

    public StageData
    (
        Vector3 playerPosition,
        Quaternion playerRotation,
        (Vector3, Quaternion)[] kickPositions)
    {
        PlayerPosition = playerPosition;
        PlayerRotation = playerRotation;
        KickPositions = kickPositions;
    }
}
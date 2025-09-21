using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MessagePack;
using Structure.Global;
using Structure.InGame.Stage;
using UnityEngine;

namespace View.Utility
{
    /// <summary>
    /// ゲーム開始時、最初に読み込まれる。
    /// このデータを参照して
    /// * チュートリアルへの移動
    /// * ステージセレクトでのクリア演出
    /// が行われる
    /// </summary>
    [MessagePackObject]
    public struct UserStateDto
    {
        [Key(0)] public GameState GameState;

        /// <summary>
        /// `PlayerState`が`StageCleared`の場合にクリアしたステージが保存される
        /// </summary>
        [Key(1)] public string ClearedStageName;

        [Key(2)] public Dictionary<string, StageProgressData> ProgressData;

        public UserStateDto(GameState state, string clearedStageName,
            Dictionary<string, StageProgressData> progressData)
        {
            GameState = state;
            ClearedStageName = clearedStageName;
            ProgressData = progressData;
        }
    }

    /// <summary>
    /// ステージセレクトシーンで見るクリア状況のデータ
    /// </summary>
    [MessagePackObject]
    public class StageProgressDto
    {
        [Key(0)] public StageState StageState;
        [Key(1)] public int BestKickCount;

        public StageProgressDto(StageState state, int bestKickCount)
        {
            StageState = state;
            BestKickCount = bestKickCount;
        }
    }

    /// <summary>
    /// ステージ中断時に生成されるセーブデータ
    /// </summary>
    [MessagePackObject]
    public struct StageDto
    {
        [Key(0)] public Vector3 PlayerPosition;
        [Key(1)] public Quaternion PlayerRotation;
        [Key(2)] public (Vector3, Quaternion)[] KickPositions;

        public StageDto
        (
            Vector3 playerPosition,
            Quaternion playerRotation,
            (Vector3, Quaternion)[] kickPositions
        )
        {
            PlayerPosition = playerPosition;
            PlayerRotation = playerRotation;
            KickPositions = kickPositions;
        }
    }

    public static class Extension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UserStateDto Convert(this UserState state)
        {
            return new UserStateDto(state.GameState, state.ClearedStageName, state.ProgressData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UserState Convert(this UserStateDto state)
        {
            return new UserState(state.GameState, state.ClearedStageName, state.ProgressData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StageDto Convert(this StageData state)
        {
            return new StageDto(state.PlayerPosition, state.PlayerRotation, state.KickPositions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StageData Convert(this StageDto state)
        {
            return new StageData(state.PlayerPosition, state.PlayerRotation, state.KickPositions);
        }
    }
}
using System.Collections.Generic;
using MessagePack;
using Structure.Global;
using Structure.InGame.Stage;

namespace Repository
{
    /// <summary>
    /// ゲーム開始時、最初に読み込まれる。
    /// このデータを参照して
    /// * チュートリアルへの移動
    /// * ステージセレクトでのクリア演出
    /// が行われる
    /// </summary>
    [MessagePackObject]
    public record struct UserStateDto
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
}
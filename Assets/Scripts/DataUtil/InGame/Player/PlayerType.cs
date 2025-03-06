using System;

namespace DataUtil.InGame.Player
{
    /// <summary>
    /// プレイヤーの状態を示す
    /// </summary>
    [Flags]
    public enum PlayerStateType
    {
        /// <summary>
        /// 静止している状態
        /// </summary>
        Idle = 0b0000_0001,
        
        /// <summary>
        /// 引っ張っている状態
        /// </summary>
        Charging = 0b0000_0010,

        /// <summary>
        /// 動いている状態
        /// </summary>
        Frying = 0b0000_0100,
        
        /// <summary>
        /// ヘルプ等で操作を受け付けない状態
        /// </summary>
        Stopping = 0b0000_1000,
    }
}
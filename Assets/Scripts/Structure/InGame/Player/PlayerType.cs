namespace Structure.InGame.Player
{
    /// <summary>
    /// プレイヤーの状態を示す
    /// </summary>
    public enum PlayerStateType
    {
        /// <summary>
        /// 静止している状態
        /// </summary>
        Idle,

        /// <summary>
        /// 引っ張っている状態
        /// </summary>
        Aiming,

        /// <summary>
        /// 動いている状態
        /// </summary>
        Frying,

        /// <summary>
        /// ヘルプ等で操作を受け付けない状態
        /// </summary>
        Stopping,
    }
}
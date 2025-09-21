using System;

namespace Structure.InGame.Stage;

[Flags]
public enum StageState
{
    /// <summary>
    /// クリアされたならば、フラグを建てる
    /// </summary>
    IsCleared = 0b0000_0001,

    /// <summary>
    /// ステージ進行途中ならば、フラグを建てる
    /// </summary>
    InProgress = 0b0000_0010,
}
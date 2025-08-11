namespace Structure.InGame.Player;

/// <summary>
/// プレイヤーに対して処理を要求するためのコマンド
/// </summary>
public readonly struct PlayerInteractCommand
{
    public CommandType Type { get; }

    public PlayerInteractCommand
    (
        CommandType type
    )
    {
        Type = type;
    }
}

public enum CommandType
{
    Undo,
}
namespace Structure.InGame.Player;

/// <summary>
/// プレイヤーに対して処理を要求するためのコマンド
/// </summary>
public interface IPlayerInteractCommand
{
}

public readonly record struct PlayerUndoCommand : IPlayerInteractCommand;
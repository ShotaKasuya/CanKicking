using System.Collections.Generic;
using Interface.InGame.Primary;

namespace Logic.InGame.Primary;

public class GameRestartLogic : IGameRestartLogic
{
    public GameRestartLogic
    (
        IReadOnlyList<IResetable> resetables
    )
    {
        Resetables = resetables;
    }

    public void RestartGame()
    {
        for (int i = 0; i < Resetables.Count; i++)
        {
            Resetables[i].Reset();
        }
    }

    private IReadOnlyList<IResetable> Resetables { get; }
}
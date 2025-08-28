using System.Collections.Generic;
using Interface.Logic.InGame;
using Interface.Model.InGame;

namespace Logic.InGame.Primary;

public class GameRestartLogic : IGameRestartLogic
{
    public GameRestartLogic
    (
        IReadOnlyList<IResetable> resetables,
        IReadOnlyList<IResetableModel> resetableModels
    )
    {
        Resetables = resetables;
        ResetableModels = resetableModels;
    }

    public void RestartGame()
    {
        for (int i = 0; i < Resetables.Count; i++)
        {
            Resetables[i].Reset();
        }

        for (int i = 0; i < ResetableModels.Count; i++)
        {
            ResetableModels[i].Reset();
        }
    }

    private IReadOnlyList<IResetable> Resetables { get; }
    private IReadOnlyList<IResetableModel> ResetableModels { get; }
}
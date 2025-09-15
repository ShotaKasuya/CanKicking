using Interface.Logic.InGame;

namespace Tests.EditMode.Logic.InGame
{
    public class MockGameRestartLogic : IGameRestartLogic
    {
        public bool IsRestarted { get; private set; }
        public void RestartGame() => IsRestarted = true;
    }
}
using Interface.InGame.Primary;

namespace Logic.InGame.Primary;

public class InGameRestartLogic: IGameRestartLogic
{
    public InGameRestartLogic
    (
        ILazyPlayerView playerView,
        ILazyStartPositionView startPositionView
    )
    {
        PlayerView = playerView;
        StartPositionView = startPositionView;
    }

    public void RestartGame()
    {
        var startPosition = StartPositionView.StartPosition.Unwrap().StartPosition.position;
        
        PlayerView.PlayerView.Unwrap().ResetPosition(startPosition);
    }
    
    private ILazyPlayerView PlayerView { get; }
    private ILazyStartPositionView StartPositionView { get; }
}
using Interface.InGame.Player;
using Interface.InGame.Primary;
using Interface.InGame.Stage;
using Module.Option;

namespace View.InGame.Primary
{
    /// <summary>
    /// プレイヤーが持つView
    /// </summary>
    public class LazyPlayerView : ILazyPlayerView
    {
        public LazyPlayerView()
        {
            PlayerView = new OnceCell<IPlayerView>();
        }

        public OnceCell<IPlayerView> PlayerView { get; }
    }

    /// <summary>
    /// ステージが持つView
    /// </summary>
    public class LazyStageView : ILazyStartPositionView, ILazyBaseHeightView, ILazyGoalHeightView
    {
        public LazyStageView()
        {
            StartPosition = new OnceCell<ISpawnPositionView>();
            BaseHeight = new OnceCell<float>();
            GoalHeight = new OnceCell<float>();
        }

        public OnceCell<ISpawnPositionView> StartPosition { get; }
        public OnceCell<float> BaseHeight { get; }
        public OnceCell<float> GoalHeight { get; }
    }
}
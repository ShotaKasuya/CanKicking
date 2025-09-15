using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.View.InGame.UserInterface;
using R3;
using Tests.Mock.Utility;

namespace Tests.Mock.InGame
{
    // ============================================================================================
    // Normal
    // ============================================================================================
    public class MockNormalUiView : INormalUiView
    {
        public bool IsShown { get; private set; }

        public UniTask Show(CancellationToken token)
        {
            IsShown = true;
            return UniTask.CompletedTask;
        }

        public UniTask Hide(CancellationToken token)
        {
            IsShown = false;
            return UniTask.CompletedTask;
        }
    }

    public class MockStopButtonView : AbstractMockEventer<Unit>, IStopButtonView
    {
    }

    public class MockProgressUiView : IProgressUiView
    {
        public float? Progress { get; private set; }
        public void SetProgress(float progress) => Progress = progress;
    }

    public class MockKickCountUiView : IKickCountUiView
    {
        public int? Count { get; private set; }
        public void SetCount(int count) => Count = count;
    }

    // ============================================================================================
    // Goal
    // ============================================================================================
    public class MockGoalUiView : IGoalUiView
    {
        public bool IsShown { get; private set; }

        public UniTask Show(CancellationToken token)
        {
            IsShown = true;
            return UniTask.CompletedTask;
        }

        public UniTask Hide(CancellationToken token)
        {
            IsShown = false;
            return UniTask.CompletedTask;
        }
    }

    public class MockRestartButtonView : AbstractMockEventer<string>, IGoal_RestartButtonView, IStop_RestartButtonView
    {
    }

    public class MockStageSelectButtonView : AbstractMockEventer<string>, IGoal_StageSelectButtonView,
        IStop_StageSelectButtonView
    {
    }

    public class MockStopUiView : IStopUiView
    {
        public bool IsShown { get; private set; }

        public UniTask Show(CancellationToken token)
        {
            IsShown = true;
            return UniTask.CompletedTask;
        }

        public UniTask Hide(CancellationToken token)
        {
            IsShown = false;
            return UniTask.CompletedTask;
        }
    }

    public class MockPlayButtonView : AbstractMockEventer<Unit>, IPlayButtonView
    {
    }
}
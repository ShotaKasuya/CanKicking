using Module.SceneReference;
using R3;

namespace Interface.InGame.UserInterface
{
    public interface IGoalUiView
    {
        public void Show();
    }

    public interface IGoal_RestartButtonView
    {
        public Observable<string> Performed { get; }
    }

    public interface IGoal_StageSelectButtonView
    {
        public Observable<string> Performed { get; }
    }
}
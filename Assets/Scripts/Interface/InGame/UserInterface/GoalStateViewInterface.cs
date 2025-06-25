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
        public Observable<SceneReference> Performed { get; }
    }

    public interface IGoal_StageSelectButtonView
    {
        public Observable<SceneReference> Performed { get; }
    }
}
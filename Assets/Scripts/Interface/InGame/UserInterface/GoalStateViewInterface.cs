using Cysharp.Threading.Tasks;
using R3;

namespace Interface.InGame.UserInterface
{
    public interface IGoalUiView
    {
        public UniTask Show();
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
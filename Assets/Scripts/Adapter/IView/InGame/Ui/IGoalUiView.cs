using Cysharp.Threading.Tasks;
using R3;
using Structure.Scene;

namespace Adapter.IView.InGame.Ui
{
    public interface IGoalUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface IGoalStateStageSelectButtonView
    {
        public Observable<SceneType> Performed { get; }
    }

    public interface IGoalStateReStartButtonView
    {
        public Observable<SceneType> Performed { get; }
    }
}
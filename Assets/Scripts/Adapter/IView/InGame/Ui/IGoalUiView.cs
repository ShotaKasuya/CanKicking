using Cysharp.Threading.Tasks;

namespace Adapter.IView.InGame.Ui
{
    public interface IGoalUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface IGoalStateStageSelectButtonView: IStageSelectButtonView
    {
    }

    public interface IGoalStateReStartButtonView: IReStartButtonView
    {
    }
}